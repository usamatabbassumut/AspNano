
using AspNano.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNano.Common.HelperClasses;
using AspNano.Infrastructure.Multitenancy;
using AspNano.Domain.Entities.Common;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AspNano.Infrastructure.Persistence
{


    //move seed and application user model to their own classes/locations

    public class ApplicationUser : IdentityUser,IMustHaveTenant
    {
        //[ForeignKey("Tenant")]
        //public Guid TenantId { get; set; } //use the tenant key (string) instead of the GUID for simplicity in all tenant isolation columns
        //public virtual TenantEntity Tenant { get; set; } //FK not necessary for tenants

        //-- Create these fields for user

        //public string FirstName { get; set; }  
        //public string LastName { get; set; }
        //public bool IsActive { get; set; }
        public string TenantId { get; set; }
    }

    public class ApplicationRoles : IdentityRole
    {

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public string CurrentTenant { get; set; }
        //Put this into the constructor -- , ITenantService tenantService
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ITenantService _tenantService;

        public ApplicationDbContext(ITenantService tenantService,DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _contextAccessor= httpContextAccessor;
            _tenantService = tenantService;
            CurrentTenant = _tenantService?.GetCurrentTenant()?.Id; //<-------- FIX THIS
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().HasKey(m => m.Id);
            builder.Entity<IdentityRole>().HasKey(m => m.Id);
            builder.Entity<VenueEntity>().HasQueryFilter(a => a.TenantId == CurrentTenant); //if you remove this, you will get all the venues
            //builder.Entity<ApplicationUser>().HasQueryFilter(a => a.TenantId == CurrentTenant); 

            //<----- This is the important part we need to get working
            //This query filter will be applied to all tables needing tenant separation


            //builder.Entity<ApplicationUser>().HasQueryFilter(a => a.TenantId.ToString() == TenantUserInfo.TenantID); 

            base.OnModelCreating(builder);
            this.SeedTenants(builder);
            this.SeedUsers(builder);
            this.SeedRoles(builder);
            this.SeedUserRoles(builder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {

            foreach (var entry in ChangeTracker.Entries<IMustHaveTenant>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    case EntityState.Modified:
                        entry.Entity.TenantId = CurrentTenant;
                        break;
                }
            }
            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }

        private void SeedTenants(ModelBuilder builder)
        {
           builder.Entity<TenantEntity>().HasData(
           new TenantEntity
           {        
               Id = Guid.Parse("297af0a9-060d-4ac7-b014-e421588150a0"),
               Key = "root",
               IsActive = true,
           }
       );
        }

        private void SeedUsers(ModelBuilder builder)
        {

            var user = new ApplicationUser
            {
                Id = "297af0a9-060d-4ac7-b014-e421588150a0",
                Email = "admin@root.com",
                NormalizedEmail = "admin@root.com",
                UserName = "adminRoot",
                NormalizedUserName = "ADMINROOT",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                TenantId= "297af0a9-060d-4ac7-b014-e421588150a0",

            };


            var password = new PasswordHasher<ApplicationUser>();
            var hashed = password.HashPassword(user, "Password123!");
            user.PasswordHash = hashed;

            builder.Entity<ApplicationUser>().HasData(user);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            //Isnt there a way to isolate the root tenant by their key? I would rather not create a root Role per each tenant
            //Also the IDs for roles do not need to inherit the base entity (and use GUIDs), could use a simple integer in this case
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "b761c12f-4f47-4722-9974-8bf705e4626b", Name = "root", ConcurrencyStamp = "1", NormalizedName = "root" },
                new IdentityRole() { Id = "6fef51e2-e448-442a-90bb-0953200acd95", Name = "admin", ConcurrencyStamp = "2", NormalizedName = "admin" },
                new IdentityRole() { Id = "5dda1b52-5d12-460c-b850-f06042399356", Name = "editor", ConcurrencyStamp = "3", NormalizedName = "editor" },
                new IdentityRole() { Id = "a2a944eb-c572-4241-99c5-7d3c2b82cc46", Name = "basic", ConcurrencyStamp = "4", NormalizedName = "basic" }
                );
        }

        private void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { RoleId = "b761c12f-4f47-4722-9974-8bf705e4626b", UserId = "297af0a9-060d-4ac7-b014-e421588150a0" }
                );
        }


        public DbSet<TenantEntity> Tenant { get; set; }
        public DbSet<VenueEntity> Venue { get; set; }


    }
}
