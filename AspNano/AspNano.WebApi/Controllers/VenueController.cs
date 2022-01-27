using AspNano.Application.Services.VenueService;
using AspNano.DTOs.VenueDTOs;
using AspNano.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNano.WebApi.Controllers
{
    [Authorize(Roles = "root")]
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
        public async Task<IActionResult> SaveVenue(VenueDTO modal)
        {
            try
            {
                bool result = await _venueService.SaveUpdateVenue(modal);
                return this.StatusCode((int)StatusCodeEnum.Ok, result);
            }
            catch (Exception ex)
            {
                return this.StatusCode((int)StatusCodeEnum.Conflict, ex.Message);
            }
        }
    }
}
