using AspNano.Application.Repository.VenueRepository;
using AspNano.DTOs.VenueDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace AspNano.Application.Services.VenueService
{
    public class VenueService : IVenueService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IVenueRepository _venueRepository;


        public VenueService(IVenueRepository venueRepository, IHttpContextAccessor _httpContextAccessor)
        {
            _venueRepository = venueRepository;
            httpContextAccessor = _httpContextAccessor;
        }


        public async Task<bool> SaveVenue(CreateVenueDTO modal)
        {
            return await _venueRepository.SaveVenue(modal);
        }

        public async Task<bool> UpdateVenue(UpdateVenueDTO modal)
        {
            return await _venueRepository.UpdateVenue(modal);

        }


        public  List<UpdateVenueDTO> GetAllVenues()
        {
            var tenantId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x=>x.Type=="tenantId").Value;
            return  _venueRepository.GetAllVenues().Include(x=>x.Tenant).Where(x=>x.TenantId==Guid.Parse(tenantId)).Select(x => new UpdateVenueDTO
            {
                VenueName = x.VenueName,
                VenueDescription = x.VenueDescription,
                TenantId = x.TenantId,
            }).ToList();
        }
    }
}
