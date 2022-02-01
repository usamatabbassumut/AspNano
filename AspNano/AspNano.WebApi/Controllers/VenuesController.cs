using AspNano.Application.Services.VenueService;
using AspNano.DTOs.VenueDTOs;
using AspNano.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNano.WebApi.Controllers
{
 
    [Authorize(Roles = "root,admin")]

    [Route("api/[controller]")]

    [ApiController]
    public class VenuesController : ControllerBase
    {
        private readonly IVenueService _venueService;

        public VenuesController(IVenueService venueService)
        {
            _venueService = venueService;
        }


        [HttpGet]
        public List<VenueDTO> GetAll()
        {
            var list = _venueService.GetAllVenuesAsync(); //use the Async name convention when using async methods
            return list;
        }


        [HttpPost]
        public async Task<IActionResult> SaveAsync(CreateVenueRequest request)
        {
            try
            {
                bool result = await _venueService.SaveVenueAsync(request);
                return this.StatusCode((int)StatusCodeEnum.Ok, result);
            }
            catch (Exception ex)
            {
                return this.StatusCode((int)StatusCodeEnum.Conflict, ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(UpdateVenueRequest request, Guid id)
        {
            try
            {
                bool result = await _venueService.UpdateVenueAsync(request, id);
                return this.StatusCode((int)StatusCodeEnum.Ok, result); //return GUID when success
            }
            catch (Exception ex)
            {
                return this.StatusCode((int)StatusCodeEnum.Conflict, ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid Id)
        {
            try
            {
                bool result = await _venueService.DeleteVenueAsync(Id);
                return this.StatusCode((int)StatusCodeEnum.Ok, result);
            }
            catch (Exception ex)
            {
                return this.StatusCode((int)StatusCodeEnum.Conflict, ex.Message);
            }
        }


    }
}
