using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TANGOCCONG.ANUIShop.Data.Entities;
using TANGOCCONG.ANUIShop.Data.Enums;

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

            modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                Name = "Danh mục 1",
                IsDelete = false,
                IsShowOnHome = true,
                ParentId = 0,
                Level = 0,
                SortOrder = 1,
                Status = Status.Active
            },
            new Category
            {
                Id = 2,
                Name = "Danh mục 2",
                IsDelete = false,
                IsShowOnHome = true,
                ParentId = 0,
                Level = 0,
                SortOrder = 2,
                Status = Status.Active
            },
            new Category
            {
                Id = 3,
                Name = "Danh mục 3",
                IsDelete = false,
                IsShowOnHome = true,
                ParentId = 0,
                Level = 0,
                SortOrder = 3,
                Status = Status.Active
            });

            modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Name = "Sách 01",
                IsDeleted = false,
                Code = "SACH0001",
                Description = "Sách 01",
                ImageId = 0,
                Price = 100000,
                IsActive = true,
                Title = "Sách 01"
            },
            new Product
            {
                Id = 2,
                Name = "Sách 02",
                IsDeleted = false,
                Code = "SACH0002",
                Description = "Sách 01",
                ImageId = 0,
                Price = 100000,
                IsActive = true,
                Title = "Sách 01"
            },
            new Product
            {
                Id = 3,
                Name = "Sách 03",
                IsDeleted = false,
                Code = "SACH0003",
                Description = "Sách 03",
                ImageId = 0,
                Price = 100000,
                IsActive = true,
                Title = "Sách 01"
            },
            new Product
            {
                Id = 4,
                Name = "Sách 04",
                IsDeleted = false,
                Code = "SACH0004",
                Description = "Sách 04",
                ImageId = 0,
                Price = 100000,
                IsActive = true,
                Title = "Sách 01"
            },
            new Product
            {
                Id = 5,
                Name = "Sách 05",
                IsDeleted = false,
                Code = "SACH0005",
                Description = "Sách 03",
                ImageId = 0,
                Price = 100000,
                IsActive = true,
                Title = "Sách 01"
            }, 
            new Product
            {
                Id = 6,
                Name = "Sách 06",
                IsDeleted = false,
                Code = "SACH0006",
                Description = "Sách 03",
                ImageId = 0,
                Price = 100000,
                IsActive = true,
                Title = "Sách 01"
            });

            modelBuilder.Entity<ProductInCategory>().HasData(
            new ProductInCategory
            {
                CategoryId = 1,
                ProductId = 1
            },
            new ProductInCategory
            {
                CategoryId = 2,
                ProductId = 2
            },
            new ProductInCategory
            {
                CategoryId = 3,
                ProductId = 3
            },
            new ProductInCategory
            {
                CategoryId = 1,
                ProductId = 4
            },
            new ProductInCategory
            {
                CategoryId = 1,
                ProductId = 5
            },
            new ProductInCategory
            {
                CategoryId = 1,
                ProductId = 6
            });
        }
    }
}
