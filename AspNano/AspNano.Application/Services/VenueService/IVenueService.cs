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
        Task<Guid> SaveVenueAsync(CreateVenueRequest modal);
        Task<Guid> UpdateVenueAsync(UpdateVenueRequest modal, Guid id);
        Task<bool> DeleteVenueAsync(Guid Id);

        PagedResponse<List<VenueDTO>> GetAllVenuesAsync(PaginationFilter filter);

    }
}
