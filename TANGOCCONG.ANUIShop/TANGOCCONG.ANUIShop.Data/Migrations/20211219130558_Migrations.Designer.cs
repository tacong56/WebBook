﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TANGOCCONG.ANUIShop.Data.EF;

namespace TANGOCCONG.ANUIShop.Data.Migrations
{
    [DbContext(typeof(ANUIShopDbContext))]
    [Migration("20211219130558_Migrations")]
    partial class Migrations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AppRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AppUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("longtext");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("longtext");

                    b.HasKey("UserId");

                    b.ToTable("AppUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("AppUserRoles");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId");

                    b.ToTable("AppUserTokens");
                });

            modelBuilder.Entity("TANGOCCONG.ANUIShop.Data.Entities.AppRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("AppRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConcurrencyStamp = "26f1d760-7632-4491-9c11-3b3a166d64e4",
                            Description = "Admintrator role",
                            Name = "Admin",
                            NormalizedName = "admin"
                        },
                        new
                        {
                            Id = 2,
                            ConcurrencyStamp = "bf473bf2-b687-441c-b7e1-a2ba18ee01be",
                            Description = "Employee role",
                            Name = "Nhân viên",
                            NormalizedName = "employee"
                        },
                        new
                        {
                            Id = 3,
                            ConcurrencyStamp = "cf220701-9b6e-4cd4-9f6f-398561fa9804",
                            Description = "Customer role",
                            Name = "Khách hàng",
                            NormalizedName = "customer"
                        });
                });

            modelBuilder.Entity("TANGOCCONG.ANUIShop.Data.Entities.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("ImageId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("longtext");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2021, 12, 19, 20, 5, 57, 423, DateTimeKind.Local).AddTicks(7778));

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("AppUsers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "b2fbc2c8-a1fa-41a4-b78f-65ad4ce19395",
                            Dob = new DateTime(1997, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "tacong56@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Ta",
                            ImageId = 0,
                            IsDelete = false,
                            LastName = "Cong",
                            LockoutEnabled = false,
                            NormalizedEmail = "tacong56@gmail.com",
                            NormalizedUserName = "admin",
                            PasswordHash = "AQAAAAEAACcQAAAAEDPg3NOH6g626RDdlLxJqbpLqDHLqSZI2mMKc7e2Tnrhvl0z0bd+KMM5GZsTvzP+vQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "",
                            State = 0,
                            TimeCreated = new DateTime(2021, 12, 19, 20, 5, 57, 486, DateTimeKind.Local).AddTicks(6130),
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        });
                });

            modelBuilder.Entity("TANGOCCONG.ANUIShop.Data.Entities.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("TANGOCCONG.ANUIShop.Data.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDelete")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsShowOnHome")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(true);

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ParentId")
                        .HasColumnType("int");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsDelete = false,
                            IsShowOnHome = true,
                            Level = 0,
                            Name = "Danh mục 1",
                            ParentId = 0,
                            SortOrder = 1,
                            Status = 0
                        },
                        new
                        {
                            Id = 2,
                            IsDelete = false,
                            IsShowOnHome = true,
                            Level = 0,
                            Name = "Danh mục 2",
                            ParentId = 0,
                            SortOrder = 2,
                            Status = 0
                        },
                        new
                        {
                            Id = 3,
                            IsDelete = false,
                            IsShowOnHome = true,
                            Level = 0,
                            Name = "Danh mục 3",
                            ParentId = 0,
                            SortOrder = 3,
                            Status = 0
                        });
                });

            modelBuilder.Entity("TANGOCCONG.ANUIShop.Data.Entities.District", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(6)
                        .HasColumnType("varchar(6)");

                    b.Property<string>("Name")
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.Property<string>("Province_Id")
                        .HasMaxLength(6)
                        .HasColumnType("varchar(6)");

                    b.Property<string>("Type")
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.HasKey("Id");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("TANGOCCONG.ANUIShop.Data.Entities.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Caption")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsMain")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsProduct")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<double>("Size")
                        .HasColumnType("double");

                    b.Property<DateTime>("TimeCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2021, 12, 19, 20, 5, 57, 427, DateTimeKind.Local).AddTicks(3365));

                    b.Property<string>("UrlPath")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("TANGOCCONG.ANUIShop.Data.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ShipAddress")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("ShipEmail")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ShipName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("ShipPhoneNumber")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("TANGOCCONG.ANUIShop.Data.Entities.OrderDetail", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("TANGOCCONG.ANUIShop.Data.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<int>("ImageId")
                        .HasColumnType("int");

                    b.Property<string>("ImageIds")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("TimeCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2021, 12, 19, 20, 5, 57, 454, DateTimeKind.Local).AddTicks(7330));

                    b.Property<DateTime>("TimeUpdated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.Property<int>("UserUpdate")
                        .HasColumnType("int");

                    b.Property<int>("View")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "SACH0001",
                            Description = "Sách 01",
                            ImageId = 0,
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Sách 01",
                            Price = 100000m,
                            TimeCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TimeUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Sách 01",
                            UserUpdate = 0,
                            View = 0
                        },
                        new
                        {
                            Id = 2,
                            Code = "SACH0002",
                            Description = "Sách 01",
                            ImageId = 0,
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Sách 02",
                            Price = 100000m,
                            TimeCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TimeUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Sách 01",
                            UserUpdate = 0,
                            View = 0
                        },
                        new
                        {
                            Id = 3,
                            Code = "SACH0003",
                            Description = "Sách 03",
                            ImageId = 0,
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Sách 03",
                            Price = 100000m,
                            TimeCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TimeUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Sách 01",
                            UserUpdate = 0,
                            View = 0
                        },
                        new
                        {
                            Id = 4,
                            Code = "SACH0004",
                            Description = "Sách 04",
                            ImageId = 0,
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Sách 04",
                            Price = 100000m,
                            TimeCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TimeUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Sách 01",
                            UserUpdate = 0,
                            View = 0
                        },
                        new
                        {
                            Id = 5,
                            Code = "SACH0005",
                            Description = "Sách 03",
                            ImageId = 0,
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Sách 05",
                            Price = 100000m,
                            TimeCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TimeUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Sách 01",
                            UserUpdate = 0,
                            View = 0
                        },
                        new
                        {
                            Id = 6,
                            Code = "SACH0006",
                            Description = "Sách 03",
                            ImageId = 0,
                            IsActive = true,
                            IsDeleted = false,
                            Name = "Sách 06",
                            Price = 100000m,
                            TimeCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TimeUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Sách 01",
                            UserUpdate = 0,
                            View = 0
                        });
                });

            modelBuilder.Entity("TANGOCCONG.ANUIShop.Data.Entities.ProductImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Caption")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("FileSize")
                        .HasColumnType("bigint");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImages");
                });

            modelBuilder.Entity("TANGOCCONG.ANUIShop.Data.Entities.ProductInCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("CategoryId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductInCategories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            ProductId = 1
                        },
                        new
                        {
                            CategoryId = 2,
                            ProductId = 2
                        },
                        new
                        {
                            CategoryId = 3,
                            ProductId = 3
                        },
                        new
                        {
                            CategoryId = 1,
                            ProductId = 4
                        },
                        new
                        {
                            CategoryId = 1,
                            ProductId = 5
                        },
                        new
                        {
                            CategoryId = 1,
                            ProductId = 6
                        });
                });

            modelBuilder.Entity("TANGOCCONG.ANUIShop.Data.Entities.Promotion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("ApplyForAll")
                        .HasColumnType("tinyint(1)");

                    b.Property<decimal?>("DiscountAmount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("DiscountPercent")
                        .HasColumnType("int");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ProductCategoryIds")
                        .HasColumnType("longtext");

                    b.Property<string>("ProductIds")
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Promotions");
                });

            modelBuilder.Entity("TANGOCCONG.ANUIShop.Data.Entities.Province", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(6)
                        .HasColumnType("varchar(6)");

                    b.Property<string>("Name")
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.Property<string>("Type")
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.HasKey("Id");

                    b.ToTable("Provinces");
                });

            modelBuilder.Entity("TANGOCCONG.ANUIShop.Data.Entities.SystemParameter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("ParameterId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("SystemParameters");
                });

            modelBuilder.Entity("TANGOCCONG.ANUIShop.Data.Entities.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("ExternalTransactionId")
                        .HasColumnType("longtext");

                    b.Property<decimal>("Fee")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Message")
                        .HasColumnType("longtext");

                    b.Property<string>("Provider")
                        .HasColumnType("longtext");

                    b.Property<string>("Result")
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("TANGOCCONG.ANUIShop.Data.Entities.Ward", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(6)
                        .HasColumnType("varchar(6)");

                    b.Property<string>("District_Id")
                        .HasMaxLength(6)
                        .HasColumnType("varchar(6)");

                    b.Property<string>("Name")
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.Property<string>("Type")
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.HasKey("Id");

                    b.ToTable("Wards");
                });

            modelBuilder.Entity("TANGOCCONG.ANUIShop.Data.Entities.Cart", b =>
                {
                    b.HasOne("TANGOCCONG.ANUIShop.Data.Entities.Product", "Product")
                        .WithMany("Carts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TANGOCCONG.ANUIShop.Data.Entities.AppUser", "AppUser")
                        .WithMany("Carts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("TANGOCCONG.ANUIShop.Data.Entities.Order", b =>
                {
                    b.HasOne("TANGOCCONG.ANUIShop.Data.Entities.AppUser", "AppUser")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("TANGOCCONG.ANUIShop.Data.Entities.OrderDetail", b =>
                {
                    b.HasOne("TANGOCCONG.ANUIShop.Data.Entities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TANGOCCONG.ANUIShop.Data.Entities.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("TANGOCCONG.ANUIShop.Data.Entities.ProductImage", b =>
                {
                    b.HasOne("TANGOCCONG.ANUIShop.Data.Entities.Product", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("TANGOCCONG.ANUIShop.Data.Entities.ProductInCategory", b =>
                {
                    b.HasOne("TANGOCCONG.ANUIShop.Data.Entities.Category", "Category")
                        .WithMany("ProductInCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TANGOCCONG.ANUIShop.Data.Entities.Product", "Product")
                        .WithMany("ProductInCategories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("TANGOCCONG.ANUIShop.Data.Entities.Transaction", b =>
                {
                    b.HasOne("TANGOCCONG.ANUIShop.Data.Entities.AppUser", "AppUser")
                        .WithMany("Transactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("TANGOCCONG.ANUIShop.Data.Entities.AppUser", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("Orders");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("TANGOCCONG.ANUIShop.Data.Entities.Category", b =>
                {
                    b.Navigation("ProductInCategories");
                });

            modelBuilder.Entity("TANGOCCONG.ANUIShop.Data.Entities.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("TANGOCCONG.ANUIShop.Data.Entities.Product", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("OrderDetails");

                    b.Navigation("ProductImages");

                    b.Navigation("ProductInCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
