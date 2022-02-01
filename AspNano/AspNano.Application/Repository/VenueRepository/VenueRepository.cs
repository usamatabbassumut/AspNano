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

        public async Task<bool> SaveVenueAsync(CreateVenueRequest modal)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var tenantId = httpContextAccessor.HttpContext.User.FindFirst("tenantId").Value;
            bool isVenueExists =CheckExisting(modal.VenueName);
            if (isVenueExists) throw new Exception("Venue already exists.");
            //Mapping the values
            VenueEntity venue = new VenueEntity();
            venue.Id = Guid.NewGuid();
            venue.TenantId = Guid.Parse(tenantId);
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

        public async Task<bool> UpdateVenueAsync(UpdateVenueRequest modal, Guid id)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var tenantId = httpContextAccessor.HttpContext.User.FindFirst("tenantId").Value;



            //This is how its done with FSH which I like
            //look up ID, see if exists
            //do the update 
            //return the GUID if successful


            //var brand = await _repository.GetByIdAsync<Brand>(id);
            //if (brand == null) throw new EntityNotFoundException(string.Format(_localizer["brand.notfound"], id)); //dont care about localization

            //await _repository.UpdateAsync<Brand>(updatedBrand);
            //await _repository.SaveChangesAsync();
            //return await Result<Guid>.SuccessAsync(id);




            //Mapping the values
            VenueEntity venue = new VenueEntity();
            venue.Id = id; //should check if exists

            venue.TenantId = Guid.Parse(tenantId);
            venue.VenueName = modal.VenueName;
            venue.VenueType = modal.VenueType;
            venue.VenueDescription = modal.VenueDescription;
            venue.LastModifiedBy = Guid.Parse(userId);
            try
            {
                await Change(venue);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public  IQueryable<VenueEntity> GetAllVenues()
        {
          return GetAll();
        }

        public async Task<bool> DeleteVenueAsync(Guid Id)
        {
            try
            {
               await Delete(Id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
