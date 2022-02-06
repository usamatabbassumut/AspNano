using AspNano.Application.Services.TenantService;
using AspNano.DTOs.ResponseDTOs;
using AspNano.DTOs.TenantDTOs;
using AspNano.Enums;
using AspNano.WebApi.Validators.Tenants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNano.WebApi.Controllers
{
    [Authorize(Roles = "root")]
    [Route("api/[controller]")]
    [ApiController]
    public class TenantsController : ControllerBase
    {
        private readonly ITenantService _tenantService;

        public TenantsController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpGet]
        //GET
        //api/tenants
        public async Task<List<TenantDTO>> GetAllTenants()
        {
            var list =  _tenantService.GetAllTenants();
            return list;
        }


        [HttpPost]
        //POST
        //api/tenants
        public async Task<IActionResult> SaveTenant(CreateTenantRequest modal)
        {
            try
            {
                bool result = await _tenantService.SaveTenant(modal);
                return this.StatusCode((int)StatusCodeEnum.Ok, result);
            }
            catch (Exception ex)
            {
                return this.StatusCode((int)StatusCodeEnum.Conflict, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(UpdateTenantRequest request, Guid id)
        {
            try
            {
                var validate = new UpdateTenantValidator();
                var validateResult = validate.Validate(request);
                if (validateResult.IsValid)
                {
                    var result = await _tenantService.UpdateTenantAsync(request, id);
                    return this.StatusCode((int)StatusCodeEnum.Ok, result);
                }
                else
                {
                    return BadRequest(validateResult.Errors);
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode((int)StatusCodeEnum.Conflict, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ResponseDTO> DeleteTenant(Guid tenantId)
        {
            return await _tenantService.RemoveTenant(tenantId);
        }
    }
}
