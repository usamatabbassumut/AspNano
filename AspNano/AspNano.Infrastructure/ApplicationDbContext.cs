using AspNano.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNano.Infrastructure
{


    public class ApplicationUser : IdentityUser
    {
    }

    public class ApplicationRoles : IdentityRole
    {

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {




        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().HasKey(m => m.Id);
            builder.Entity<IdentityRole>().HasKey(m => m.Id);
            base.OnModelCreating(builder);
            this.SeedUsers(builder);
            this.SeedRoles(builder);
            this.SeedUserRoles(builder);
        }

        private void SeedUsers(ModelBuilder builder)
        {

            var user = new ApplicationUser
            {
                Id = "297af0a9-060d-4ac7-b014-e421588150a0",

                Email = "aspnano2022@info.com",
                NormalizedEmail = "aspnano2022@info.com",
                UserName = "aspnano",
                NormalizedUserName = "OWNER",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };


            var password = new PasswordHasher<ApplicationUser>();
            var hashed = password.HashPassword(user, "Admin*123");
            user.PasswordHash = hashed;

            builder.Entity<ApplicationUser>().HasData(user);
        }

        private void SeedRoles(ModelBuilder builder)
        {
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


        public DbSet<Tenant> Tenant { get; set; }


    }
}
