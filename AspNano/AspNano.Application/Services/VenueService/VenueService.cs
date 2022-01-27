using AspNano.Application.Repository.VenueRepository;
using AspNano.DTOs.VenueDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
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


        public async Task<bool> SaveUpdateVenue(VenueDTO modal)
        {
            return await _venueRepository.SaveUpdateVenue(modal);
        }
    }
}
