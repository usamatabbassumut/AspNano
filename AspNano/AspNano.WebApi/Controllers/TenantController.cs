using AspNano.Application.Services.TenantService;
using AspNano.DTOs.TenantDTOs;
using AspNano.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNano.WebApi.Controllers
{
    [Authorize(Roles = "root")]
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ITenantService _tenantService;

        public TenantController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpGet]
        [Route("GetTenants")]
        public async Task<List<TenantDTO>> GetAllTenants()
        {
            var list = _tenantService.GetAllTenants();
            return list;
        }

        [HttpPost]
        [Route("SaveTenant")]
        public async Task<IActionResult> SaveTenant(TenantDTO modal)
        {
            try
            {
                bool result = await _tenantService.SaveUpdateTenant(modal);
                return this.StatusCode((int)StatusCodeEnum.Ok, result);
            }
            catch (Exception ex)
            {
                return this.StatusCode((int)StatusCodeEnum.Conflict, ex.Message);
            }
        }
    }
}
