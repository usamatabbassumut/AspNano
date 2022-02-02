using AspNano.Core.Entities;
using AspNano.DTOs.ResponseDTOs;
using AspNano.DTOs.TenantDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNano.Application.Services.TenantService
{
    public interface ITenantService
    {
        List<TenantDTO> GetAllTenants();
        bool CheckExisting(string key);
        Task<bool> SaveUpdateTenant(CreateTenantRequest modal);
        Task<ResponseDTO> RemoveTenant(Guid Id);
    }
}
