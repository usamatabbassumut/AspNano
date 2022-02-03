using AspNano.Application.Repository.VenueRepository;
using AspNano.Common.ApplicationExtensions;
using AspNano.Common.HelperClasses;
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
        private readonly IVenueRepository _venueRepository;


        public VenueService(IVenueRepository venueRepository)
        {
            _venueRepository = venueRepository;
        }


        public async Task<bool> SaveVenueAsync(CreateVenueRequest modal)
        {
            return await _venueRepository.SaveVenueAsync(modal);
        }

        public async Task<Guid> UpdateVenueAsync(UpdateVenueRequest modal, Guid id)
        {
            return await _venueRepository.UpdateVenueAsync(modal, id);

        }

        //implement automapper
        //async?
        public List<VenueDTO> GetAllVenuesAsync()
        {
            return  _venueRepository.GetAllVenues().Where(x => x.TenantId == Guid.Parse(TenantUserInfo.TenantID)).Include(x=>x.Tenant).Select(x => new VenueDTO
            {
                Id = x.Id,
                VenueName = x.VenueName, 
                VenueDescription = x.VenueDescription,
                VenueType = NanoExtension.GetEnumDescription(x.VenueType), 
                VenueTypeId = (int)x.VenueType

            }).ToList();
        }

        public async Task<bool> DeleteVenueAsync(Guid Id)
        {
             //check tenant obviously
             return await _venueRepository.DeleteVenueAsync(Id);
        }
    }
}

