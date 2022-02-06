using AspNano.Application.Services.VenueService;
using AspNano.DTOs.ResponseDTOs;
using AspNano.DTOs.VenueDTOs;
using AspNano.Enums;
using AspNano.WebApi.Validators;
using AspNano.WebApi.Validators.Venues;
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


        [HttpPost("GetAllVenues")]
        public PagedResponse<List<VenueDTO>> GetAll(PaginationFilter filter)
        {
            var list = _venueService.GetAllVenuesAsync(filter); //use the Async name convention when using async methods
            return list;
        }


        [HttpPost]
        public async Task<IActionResult> SaveAsync(CreateVenueRequest request)
        {
            try
            {
                var validate = new CreateVenueValidator();
                var validateResult = validate.Validate(request);
                if (validateResult.IsValid)
                {
                    var result = await _venueService.SaveVenueAsync(request);
                    return this.StatusCode((int)StatusCodeEnum.Ok, result);

                }
                else {
                    return BadRequest(validateResult.Errors);
                }
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
                var validate = new UpdateVenueValidator();
                var validateResult = validate.Validate(request);
                if (validateResult.IsValid)
                {
                    var result = await _venueService.UpdateVenueAsync(request, id);
                    return this.StatusCode((int)StatusCodeEnum.Ok, result);
                }
                else {
                    return BadRequest(validateResult.Errors);
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode((int)StatusCodeEnum.Conflict, ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<ResponseDTO> DeleteAsync(Guid id)
        {
          return await _venueService.DeleteVenueAsync(id);
        }


    }
}
