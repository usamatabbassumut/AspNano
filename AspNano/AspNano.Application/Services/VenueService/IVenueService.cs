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
        Task<bool> SaveVenueAsync(CreateVenueRequest modal);
        Task<bool> UpdateVenueAsync(UpdateVenueRequest modal, Guid id);
        Task<bool> DeleteVenueAsync(Guid Id);

       List<VenueDTO> GetAllVenuesAsync();

    }
}
