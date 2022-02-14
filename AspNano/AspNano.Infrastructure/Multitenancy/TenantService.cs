using AspNano.Domain.Entities;
using AspNano.DTOs.ResponseDTOs;
using AspNano.DTOs.TenantDTOs;
using AspNano.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNano.Infrastructure.Multitenancy
{
    public class TenantService : ITenantService
    {
        
        //private readonly ITenantRepository _tenantRepository;
        private TenantDTO _currentTenant;
        private readonly TenantManagementDbContext _tenantManagementDbContext;
        private HttpContext _httpContext;

        public TenantService(TenantManagementDbContext tenantManagementDbContext, IHttpContextAccessor contextAccessor)
        {
            _tenantManagementDbContext = tenantManagementDbContext;

            //so everytime this instantiates, looks at the header or auth

            _httpContext = contextAccessor.HttpContext;
            var tenantFromAuth = _httpContext.User?.Claims?.FirstOrDefault(x => x.Type == "tenant")?.Value;
            if (tenantFromAuth == null)
            {
                _httpContext.Request.Headers.TryGetValue("tenant", out var tenantFromHeader);
                tenantFromAuth = tenantFromHeader;
            }

            //string tenantId = TenantResolver.Resolver(_httpContext);
            if (!string.IsNullOrEmpty(tenantFromAuth))
            {
                SetCurrentTenant(tenantFromAuth);
            }
            else
            {
                throw new Exception("Invalid Tenant!");
            }


       

        }

        //public TenantService(ITenantRepository tenantRepository)
        //{
        //    _tenantRepository = tenantRepository;
        //}

        //public bool CheckExisting(string key)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<TenantDTO> GetAllTenants()
        //{
        //    return _tenantRepository.GetAllTenants().Select(x => new TenantDTO
        //    {
        //        Id = x.Id.ToString(),
        //        Key = x.Key,
        //    }).ToList();
        //}

        //public async Task<bool> SaveTenant(CreateTenantRequest modal)
        //{
        //    return await _tenantRepository.SaveTenant(modal);
        //}

        //public async Task<Guid> UpdateTenantAsync(UpdateTenantRequest modal, Guid id)
        //{
        //    return await _tenantRepository.UpdateTenantAsync(modal, id);
        //}

        //public async Task<ResponseDTO> RemoveTenant(Guid Id)
        //{
        //    var singleTenant = await Get(Id);
        //    if (singleTenant != null)
        //    {
        //        singleTenant.IsDeleted = true;
        //        singleTenant.DeletedOn = DateTime.UtcNow;
        //        var userId = httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //        singleTenant.DeletedBy = !string.IsNullOrEmpty(userId) ? Guid.Parse(userId) : new Guid();
        //        //Updating
        //        await Change(singleTenant);

        //        return new ResponseDTO() { IsSuccessful = true, Response = "Deleted Successfully", StatusCode = 1 };
        //    }
        //    return new ResponseDTO() { IsSuccessful = false, Response = "Deleted Failed", StatusCode = 0 };
        //}
        public TenantDTO GetCurrentTenant()
        {
            return _currentTenant;
        }

        public void SetCurrentTenant(string tenant)
        {
            if (_currentTenant != null)
            {
                throw new Exception("Method reserved for in-scope initialization");
            }

            var tenantDto = new TenantDTO();
            var tenantInfo = _tenantManagementDbContext.Tenants.Where(x=>x.Key == tenant).FirstOrDefault();
            
            if (tenantInfo != null)
            {
                tenantDto.Id = tenantInfo.Id.ToString();
                tenantDto.Key = tenantInfo.Key;

            }

            if (tenantDto == null)
            {
                throw new Exception("Tenant Invalid");
            }

   

            _currentTenant = tenantDto;
       
        }

    }
}
