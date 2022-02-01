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


        public async Task<bool> SaveVenueAsync(CreateVenueRequest modal)
        {
            return await _venueRepository.SaveVenueAsync(modal);
        }

        public async Task<bool> UpdateVenueAsync(UpdateVenueRequest modal, Guid id)
        {
            return await _venueRepository.UpdateVenueAsync(modal, id);

        }

        //implement automapper
        //async?
        public List<VenueDTO> GetAllVenuesAsync()
        {
            var tenantId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x=>x.Type=="tenantId").Value; //move to middleware
            return  _venueRepository.GetAllVenues().Include(x=>x.Tenant).Where(x=>x.TenantId==Guid.Parse(tenantId)).Select(x => new VenueDTO
            {
                Id = x.Id,
                VenueName = x.VenueName, 
                VenueDescription = x.VenueDescription,
                VenueType = x.VenueType.ToString(), //?? enums 
                //want to return VenueTypeId also (integer for the client)

            }).ToList();
        }

        public async Task<bool> DeleteVenueAsync(Guid Id)
        {

            //check tenant obviously
             return await _venueRepository.DeleteVenueAsync(Id);
        }
    }
}

