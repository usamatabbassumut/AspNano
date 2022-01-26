using AspNano.Core.Entities;
using AspNano.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNano.Application.Repositories.Base;
using AspNano.Infrastructure;

namespace AspNano.Application.Repositories
{
    public interface ICompanyRepository : IAsyncRepository<Tenant, int>
    {
    }

    public class CompanyRepository : EFRepository<Tenant, int>,ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext context) : base(context)
        {

        }
    }

}
