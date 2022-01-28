using AspNano.Application.EFRepository;
using AspNano.DTOs.VenueDTOs;
using AspNano.Entities.Entities;
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
        Task<bool> SaveVenue(CreateVenueDTO modal);
        Task<bool> UpdateVenue(UpdateVenueDTO modal);

        IQueryable<VenueEntity> GetAllVenues();

    }
}
