using AspNano.DTOs.TenantDTOs;
using AspNano.DTOs.VenueDTOs;
using AspNano.Domain.Entities;
using AutoMapper;

namespace AspNano.WebApi.MappingProfiles
{
    public class ProfileMapper:Profile
    {
        public ProfileMapper()
        {
            CreateMap<CreateVenueRequest, VenueEntity>();
            CreateMap<UpdateVenueRequest, VenueEntity>();
            CreateMap<UpdateTenantRequest, TenantEntity>();
        }
    }
}
