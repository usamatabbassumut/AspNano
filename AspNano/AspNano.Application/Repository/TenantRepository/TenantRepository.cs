﻿using AspNano.Application.EFRepository;
using AspNano.Core.Entities;
using AspNano.DTOs.TenantDTOs;
using AspNano.Infrastructure;
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

        public TenantRepository(ApplicationDbContext dbContext, 
            IHttpContextAccessor _httpContextAccessor,
            UserManager<ApplicationUser> userManager
            ) : base(dbContext)
        {
            httpContextAccessor = _httpContextAccessor;
                _userManager = userManager;
        }
       public IQueryable<TenantEntity> GetAllTenants()
        {
            return GetAll();
        }

        public IEnumerable<TenantEntity> GetTenantById(Guid tenantID)
        {
            return GetWithCondition(x => x.Id == tenantID).ToList();
        }

        public bool CheckExisting(string key)
        {
            return GetWithCondition(x => x.Key.ToLower()==key.ToLower()).Any();
        }

        public async Task<bool> SaveUpdateTenant(TenantDTO modal)
        {
            bool isPresent=CheckExisting(modal.Key);
            if (isPresent) throw new Exception("Tenant already exists.");

            //Saving the Tenant
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
           


            TenantEntity tenant =new TenantEntity();
            tenant.Id = Guid.NewGuid();
            tenant.Key = modal.Key;
            tenant.CreatedBy = Guid.Parse(userId);
            try
            {
                await Add(tenant);
                var tenantId = tenant.Id;

                var userExist = await _userManager.FindByEmailAsync(modal.Email);
                if (userExist != null)
                {
                    return false;
                }

                var user = new ApplicationUser
                {
                    UserName = modal.Email,
                    Email = modal.Email,
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
    }
}
