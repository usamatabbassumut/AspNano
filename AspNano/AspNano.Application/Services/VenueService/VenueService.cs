using AspNano.Application.Repository.VenueRepository;
using AspNano.Common.ApplicationExtensions;
using AspNano.Common.HelperClasses;
using AspNano.Domain.Entities;
using AspNano.DTOs.ResponseDTOs;
using AspNano.DTOs.VenueDTOs;
using AspNano.Infrastructure.Persistence;
using AutoMapper;
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

        // FOR THE TIME BEING I AM NOT GOING TO USE REPOSITORY


        //private readonly IVenueRepository _venueRepository;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public VenueService(ApplicationDbContext dbContext, IMapper mapper)
        {
            //_venueRepository = venueRepository;
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public bool CheckExisting(string venueName)
        {
            return _dbContext.Venue.Where(x => x.VenueName.ToLower() == venueName.ToLower()).Any();
        }

        public async Task<Guid> SaveVenueAsync(CreateVenueRequest modal)
        {


            bool isVenueExists = CheckExisting(modal.VenueName);
            if (isVenueExists) throw new Exception("Venue already exists.");
            //Mapping the values
            VenueEntity venue = new VenueEntity();
            venue = _mapper.Map(modal, venue);
            venue.Id = Guid.NewGuid();

            //Will be handled on save changes in app db context
            //venue.TenantId = Guid.Parse(_tenantService.GetCurrentTenant().Id); //Getting tenant id from common helper class
            //         
            //venue.CreatedBy = Guid.Parse(TenantUserInfo.UserID);

            try
            {

                await _dbContext.Set<VenueEntity>().AddAsync(venue);
                await _dbContext.SaveChangesAsync();
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
            isPresent = _dbContext.Venue.Where(x => x.Id == id).Any();

            if (!isPresent)
                throw new Exception("Venue not found");

            //Mapping the values
            VenueEntity venue = new VenueEntity();
            venue = _mapper.Map(modal, venue);     
            venue.Id = id;
            try
            {
                _dbContext.Set<VenueEntity>().Update(venue); //make async
                _dbContext.SaveChanges();
                return venue.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //implement automapper
        //async?
        public PagedResponse<List<VenueDTO>> GetAllVenuesAsync(PaginationFilter filter)
        {

            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData= _dbContext.Set<VenueEntity>()
             .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
              .Take(validFilter.PageSize)
                //.Where(x => x.TenantId == Guid.Parse(TenantUserInfo.TenantID) && (x.IsDeleted==false||x.IsDeleted==null))
                .Select(x => new VenueDTO
            {
                Id = x.Id,
                VenueName = x.VenueName, 
                VenueDescription = x.VenueDescription,
                VenueType = NanoExtension.GetEnumDescription(x.VenueType), 
                VenueTypeId = (int)x.VenueType

            }).ToList();
            //var totalRecords =  _venueRepository.GetAllVenues().CountAsync();

            return (new PagedResponse<List<VenueDTO>>(pagedData, validFilter.PageNumber, validFilter.PageSize));

        }

        public async Task<ResponseDTO> DeleteVenueAsync(Guid id)
        {
            //not implemented
            var getVenue = await _dbContext.Set<VenueEntity>().FindAsync(id);

            if (getVenue != null)
            {
                getVenue.IsDeleted = true;
                getVenue.DeletedOn = DateTime.UtcNow;
               // var userId = httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
               // getVenue.DeletedBy = !string.IsNullOrEmpty(userId) ? Guid.Parse(userId) : new Guid();
                //Updating
                //await Change(getVenue);

                return new ResponseDTO() { IsSuccessful = true, Response = "Deleted Successfully", StatusCode = 1 };
            }
            return new ResponseDTO() { IsSuccessful = false, Response = "Deleted Failed", StatusCode = 0 };



        }
    }
}

