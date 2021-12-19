using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TANGOCCONG.ANUIShop.Data.Entities;

namespace TANGOCCONG.ANUIShop.Data.Extentions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // any guid
            //var ADMIN_ID = Guid.NewGuid();
            // any guid, but nothing is against to use the same one
            //var ROLE_ID = Guid.NewGuid();
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = 1,
                Name = "Admin",
                NormalizedName = "admin",
                Description = "Admintrator role"
            },
            new AppRole
            {
                Id = 2,
                Name = "Nhân viên",
                NormalizedName = "employee",
                Description = "Employee role"
            },
            new AppRole
            {
                Id = 3,
                Name = "Khách hàng",
                NormalizedName = "customer",
                Description = "Customer role"
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = 1,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "tacong56@gmail.com",
                NormalizedEmail = "tacong56@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "123456"),
                SecurityStamp = string.Empty,
                FirstName = "Ta",
                LastName = "Cong",
                Dob = new DateTime(1997, 06, 05),
                TimeCreated = DateTime.Now,
            });

            modelBuilder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>
            {
                RoleId = 1,
                UserId = 1
            });
        }
    }
}
