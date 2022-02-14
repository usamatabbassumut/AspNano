using AspNano.Application.EFRepository;
using AspNano.DTOs.ResponseDTOs;
using AspNano.DTOs.VenueDTOs;
using AspNano.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNano.Application.Repository.VenueRepository
{
    public interface IVenueRepository:IRepository<VenueEntity>
    {
        bool CheckExisting(string key);
        Task<Guid> SaveVenueAsync(CreateVenueRequest modal);
        Task<Guid> UpdateVenueAsync(UpdateVenueRequest modal, Guid id);
        Task<ResponseDTO> DeleteVenueAsync(Guid Id);
        IQueryable<VenueEntity> GetAllVenues();
        int GetTotalCount();
    }
}
