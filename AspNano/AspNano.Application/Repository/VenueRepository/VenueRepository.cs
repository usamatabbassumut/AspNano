using AspNano.Application.EFRepository;
using AspNano.Common.HelperClasses;
using AspNano.DTOs.ResponseDTOs;
using AspNano.DTOs.VenueDTOs;
using AspNano.Domain.Entities;
using AspNano.Infrastructure.Persistence;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AspNano.Infrastructure.Multitenancy;

namespace AspNano.Application.Repository.VenueRepository
{
    public class VenueRepository : Repository<VenueEntity>, IVenueRepository
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper _mapper;
        //private readonly ITenantService _tenantService;

        public VenueRepository(IHttpContextAccessor _httpContextAccessor, IMapper mapper, ApplicationDbContext dbContext) : base(dbContext)
        {
            httpContextAccessor = _httpContextAccessor;
            _mapper = mapper;
            //_tenantService = tenantService;
        }

        public bool CheckExisting(string venueName)
        {
            return GetWithCondition(x => x.Name.ToLower() == venueName.ToLower()).Any();
        }

        public async Task<Guid> SaveVenueAsync(CreateVenueRequest modal)
        {
            bool isVenueExists =CheckExisting(modal.VenueName);
            if (isVenueExists) throw new Exception("Venue already exists.");
            //Mapping the values
            VenueEntity venue = new VenueEntity();
            venue = _mapper.Map(modal,venue);
            venue.Id = Guid.NewGuid();
            //venue.TenantId = Guid.Parse(_tenantService.GetCurrentTenant().Id); //Getting tenant id from common helper class
            //venue.CreatedBy = Guid.Parse(TenantUserInfo.UserID);
            try
            {
                await Add(venue);
                return venue.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Guid> UpdateVenueAsync(UpdateVenueRequest modal, Guid id)
        {
            bool isPresent = false;
            isPresent= GetWithCondition(x=>x.Id==id).Any();

            if (!isPresent)
                throw new Exception("Venue not found");

            //Mapping the values
            VenueEntity venue = new VenueEntity();
            venue= _mapper.Map(modal,venue);
            //venue.TenantId = Guid.Parse(TenantUserInfo.TenantID);
            venue.Id = id; 
            venue.LastModifiedBy = Guid.Parse(TenantUserInfo.UserID);
            try
            {
                await Change(venue);
                return venue.Id;
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

        public async Task<ResponseDTO> DeleteVenueAsync(Guid Id)
        {
        

            var getVenue = await Get(Id);
            if (getVenue != null)
            {
                getVenue.IsDeleted = true;
                getVenue.DeletedOn = DateTime.UtcNow;
                var userId = httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                getVenue.DeletedBy = !string.IsNullOrEmpty(userId) ? Guid.Parse(userId) : new Guid();
                //Updating
                await Change(getVenue);

                return new ResponseDTO() { IsSuccessful = true, Response = "Deleted Successfully", StatusCode = 1 };
            }
            return new ResponseDTO() { IsSuccessful = false, Response = "Deleted Failed", StatusCode = 0 };

        }
    }
}
