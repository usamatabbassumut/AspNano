using AspNano.Application.EFRepository;
using AspNano.Core.Entities;
using AspNano.DTOs.TenantDTOs;
using AspNano.Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AspNano.Application.Repository.TenantRepository
{
    public class TenantRepository : Repository<Tenant>, ITenantRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public TenantRepository(ApplicationDbContext dbContext, IHttpContextAccessor _httpContextAccessor) : base(dbContext)
        {
            httpContextAccessor = _httpContextAccessor;
        }
       public IQueryable<Tenant> GetAllTenants()
        {
            return GetAll();
        }

        public IEnumerable<Tenant> GetTenantById(Guid tenantID)
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
            Tenant tenant =new Tenant();
            tenant.Id = Guid.NewGuid();
            tenant.Key = modal.Key;
            tenant.CreatedBy = Guid.Parse(userId);
            try
            {
                await Add(tenant);
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
