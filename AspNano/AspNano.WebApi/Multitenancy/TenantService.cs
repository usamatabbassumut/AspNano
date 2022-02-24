using AspNano.Domain.Entities;
using AspNano.DTOs.ResponseDTOs;
using AspNano.DTOs.TenantDTOs;
using AspNano.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AspNano.WebApi.Multitenancy
{
    public class TenantService : ITenantService
    {

        //private readonly ITenantRepository _tenantRepository;
        private TenantDTO _currentTenant;
        //private HttpContext _httpContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TenantService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            //_tenantManagementDbContext = tenantManagementDbContext;
            var userClaimsList = _httpContextAccessor.HttpContext?.User?.FindFirst("tenant")?.Value;

            //so everytime this instantiates, looks at the header or auth
            //_httpContext = httpContextAccessor?.HttpContext;
            //if (_httpContext?.User?.FindFirst("tenant")?.Value != null)
            //{

            //    _httpContext.Request.Headers.TryGetValue("tenant", out var tenantFromHeader);

            //    var tenantFromToken = httpContextAccessor.HttpContext?.User?.FindFirst("tenant")?.Value; 


            //    //string tenantId = TenantResolver.Resolver(_httpContext);
            //    if (!string.IsNullOrEmpty(tenantFromToken))
            //    {
            //        SetCurrentTenant(tenantFromToken);
            //    }
            //    else
            //    {
            //        throw new Exception("Invalid Tenant!");
            //    }
            //}

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
            //var tenantInfo = _tenantManagementDbContext.Tenant.Where(x=>x.Key == tenant).FirstOrDefault();
            tenantDto.Id = tenant;
            //if (tenantInfo != null)
            //{
            //    tenantDto.Id = tenantInfo.Id.ToString();
            //    tenantDto.Key = tenantInfo.Key;

            //}

            if (tenantDto == null)
            {
                throw new Exception("Tenant Invalid");
            }



            _currentTenant = tenantDto;

        }

    }
}