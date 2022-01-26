using AspNano.Application.ViewModels;
using AspNano.Core.Entities;
using AutoMapper;

namespace AspNano.WebApi.Mapping
{
    public class AutoMapper:Profile
    {
        
        public AutoMapper()
        {
            //    /* CreateMap<company, companydtp>();*/ // means you want to map from company to companydto
            //    //use for update
            //    //CreateMap<company, company>();
            CreateMap<CompanyVM, Tenant>().ReverseMap();
        }
    }
}
