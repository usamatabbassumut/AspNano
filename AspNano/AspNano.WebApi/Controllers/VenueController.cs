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
    public class VenueController : ControllerBase
    {
        private readonly IVenueService _venueService;

        public VenueController(IVenueService venueService)
        {
            _venueService = venueService;
        }
        [HttpPost]
        [Route("SaveVenue")]
        public async Task<IActionResult> SaveVenue(CreateVenueDTO modal)
        {
            try
            {
                bool result = await _venueService.SaveVenue(modal);
                return this.StatusCode((int)StatusCodeEnum.Ok, result);
            }
            catch (Exception ex)
            {
                return this.StatusCode((int)StatusCodeEnum.Conflict, ex.Message);
            }
        }

        [HttpPost]
        [Route("UpdateVenue")]
        public async Task<IActionResult> UpdateVenue(UpdateVenueDTO modal)
        {
            try
            {
                bool result = await _venueService.UpdateVenue(modal);
                return this.StatusCode((int)StatusCodeEnum.Ok, result);
            }
            catch (Exception ex)
            {
                return this.StatusCode((int)StatusCodeEnum.Conflict, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetAllVenues")]

        public  List<UpdateVenueDTO> GetAllVenues()
        {
            var list = _venueService.GetAllVenues();
            return list;
        }

    }
}
