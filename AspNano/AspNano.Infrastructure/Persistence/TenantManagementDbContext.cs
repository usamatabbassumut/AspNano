using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNano.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspNano.Infrastructure.Persistence
{
    public class TenantManagementDbContext : DbContext
    {
        public TenantManagementDbContext(DbContextOptions<TenantManagementDbContext> options)
        : base(options)
        {
        }

        public DbSet<TenantEntity> Tenants { get; set; }
    }
}
