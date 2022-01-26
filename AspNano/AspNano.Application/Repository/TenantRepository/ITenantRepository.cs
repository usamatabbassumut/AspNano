using AspNano.Application.EFRepository;
using AspNano.Core.Entities;
using AspNano.DTOs.TenantDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNano.Application.Repository.TenantRepository
{
    public interface ITenantRepository : IRepository<Tenant>
    {
        IQueryable<Tenant> GetAllTenants();
        bool CheckExisting(string key);

        Task<bool> SaveUpdateTenant(TenantDTO modal);
    }
}
