using AspNano.Application.EFRepository;
using AspNano.DTOs.VenueDTOs;
using AspNano.Entities.Entities;
using AspNano.Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AspNano.Application.Repository.VenueRepository
{
    public class VenueRepository : Repository<VenueEntity>, IVenueRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public VenueRepository(IHttpContextAccessor _httpContextAccessor, ApplicationDbContext dbContext) : base(dbContext)
        {
            httpContextAccessor = _httpContextAccessor;
        }

        public bool CheckExisting(string venueName)
        {
            return GetWithCondition(x => x.VenueName.ToLower() == venueName.ToLower()).Any();
        }

        public async Task<bool> SaveUpdateVenue(VenueDTO modal)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            bool isVenueExists =CheckExisting(modal.VenueName);
            if (isVenueExists) throw new Exception("Venue already exists.");
            //Mapping the values
            VenueEntity venue = new VenueEntity();
            venue.Id = Guid.NewGuid();
            venue.TenantId = modal.TenantId;
            venue.VenueName = modal.VenueName;
            venue.VenueType = modal.VenueType;
            venue.VenueDescription = modal.VenueDescription;
            venue.CreatedBy = Guid.Parse(userId);
            try
            {
                await Add(venue);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
