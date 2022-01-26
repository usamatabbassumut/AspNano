using AspNano.Application.Repositories;
using AspNano.Application.ViewModels;
using AspNano.Core.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNano.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        public CompanyController(ICompanyRepository companyRepository, IMapper mapper)
        {
           _companyRepository = companyRepository;
            _mapper = mapper;
        }
}
}
