﻿using AspNano.Application.Repository.TenantRepository;
using AspNano.Core.Entities;
using AspNano.DTOs.TenantDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNano.Application.Services.TenantService
{
    public class TenantService : ITenantService
    {
        private readonly ITenantRepository _tenantRepository;

        public TenantService(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        public bool CheckExisting(string key)
        {
            throw new NotImplementedException();
        }

        public List<TenantDTO> GetAllTenants()
        {
            return _tenantRepository.GetAllTenants().Select(x=>new TenantDTO
            {
                Key = x.Key,
                TenantID=x.Id
            }).ToList();
        }

        public async Task<bool> SaveUpdateTenant(TenantDTO modal)
        {
            return await _tenantRepository.SaveUpdateTenant(modal);
        }
    }
}