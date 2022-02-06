using AspNano.Application.EFRepository;
using AspNano.Core.Entities;
using AspNano.DTOs.ResponseDTOs;
using AspNano.DTOs.TenantDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNano.Application.Repository.TenantRepository
{
    public interface ITenantRepository : IRepository<TenantEntity>
    {
        IQueryable<TenantEntity> GetAllTenants();
        bool CheckExisting(string key);

        Task<bool> SaveTenant(CreateTenantRequest modal);
        Task<ResponseDTO> RemoveTenant(Guid Id);

        Task<Guid> UpdateTenantAsync(UpdateTenantRequest modal, Guid id);

    }
}
