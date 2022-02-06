using AspNano.Application.EFRepository;
using AspNano.Common.HelperClasses;
using AspNano.Core.Entities;
using AspNano.DTOs.ResponseDTOs;
using AspNano.DTOs.TenantDTOs;
using AspNano.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AspNano.Application.Repository.TenantRepository
{
    public class TenantRepository : Repository<TenantEntity>, ITenantRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public TenantRepository(ApplicationDbContext dbContext, 
            IHttpContextAccessor _httpContextAccessor,
            UserManager<ApplicationUser> userManager,
             IMapper mapper
            ) : base(dbContext)
        {
            httpContextAccessor = _httpContextAccessor;
                _userManager = userManager;
            _mapper = mapper;
        }
       public IQueryable<TenantEntity> GetAllTenants()
        {
            return GetAll().Where(x=>x.IsDeleted==false||x.IsDeleted==null);
        }

        public IEnumerable<TenantEntity> GetTenantById(Guid tenantID)
        {
            return GetWithCondition(x => x.Id == tenantID).ToList();
        }

        public bool CheckExisting(string key)
        {
            return GetWithCondition(x => x.Key.ToLower()==key.ToLower()).Any();
        }

        //This would be better to have Create and Update as seperate methods I think 
        //For tenant however, its not so important -- the only updating would be to a Boolean field we need to add (Active) - this would be used to activate and deactivate tenant accounts
        //we would never delete a tenant in the real world
        public async Task<bool> SaveTenant(CreateTenantRequest modal)
        {
            bool isPresent=CheckExisting(modal.Key);
            if (isPresent) throw new Exception("Tenant already exists.");

            //Saving the Tenant
            TenantEntity tenant =new TenantEntity();
            tenant.Id = Guid.NewGuid();
            tenant.Key = modal.Key;
            tenant.CreatedBy = Guid.Parse(TenantUserInfo.UserID);
            try
            {
                await Add(tenant);
                var tenantId = tenant.Id;

                var userExist = await _userManager.FindByEmailAsync(modal.AdminEmail);
                if (userExist != null)
                {
                    return false;
                }

                var user = new ApplicationUser
                {
                    UserName = modal.AdminEmail,
                    Email = modal.AdminEmail,
                    EmailConfirmed = false,
                    TenantId = tenantId
                };

                var result = await _userManager.CreateAsync(user, modal.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<Guid> UpdateTenantAsync(UpdateTenantRequest modal, Guid id)
        {
            bool isPresent = false;
            isPresent = GetWithCondition(x => x.Id == id).Any();

            if (!isPresent)
                throw new Exception("Venue not found");

            //Mapping the values
            TenantEntity tenant = new TenantEntity();
            tenant = _mapper.Map(modal, tenant);
            tenant.Id = id;
            tenant.LastModifiedBy = Guid.Parse(TenantUserInfo.UserID);
            try
            {
                await Change(tenant);
                return tenant.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




        public async Task<ResponseDTO> RemoveTenant(Guid Id)
        {
            var singleTenant = await Get(Id);
            if (singleTenant != null)
            {
                singleTenant.IsDeleted = true;
                singleTenant.DeletedOn = DateTime.UtcNow;
                var userId = httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                singleTenant.DeletedBy = !string.IsNullOrEmpty(userId) ? Guid.Parse(userId) : new Guid();
                //Updating
                await Change(singleTenant);
                
                return new ResponseDTO() { IsSuccessful = true, Response = "Deleted Successfully", StatusCode = 1 };
            }
             return new ResponseDTO() { IsSuccessful = false, Response = "Deleted Failed", StatusCode = 0 };
        }


    }
}
