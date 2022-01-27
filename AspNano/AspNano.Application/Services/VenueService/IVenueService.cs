using AspNano.DTOs.VenueDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNano.Application.Services.VenueService
{
    public interface IVenueService
    {
        Task<bool> SaveUpdateVenue(VenueDTO modal);
    }
}
