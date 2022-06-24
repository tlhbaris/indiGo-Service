﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using indiGo.Data.EntityFramework;

#nullable disable

namespace indiGo.Data.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("indiGo.Core.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AddressInfo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApartmentNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FlatNo")
                        .HasColumnType("int");

                    b.Property<string>("Neighborhood")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("indiGo.Core.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<decimal>("Price")
                        .HasPrecision(8, 2)
                        .HasColumnType("decimal(8,2)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("indiGo.Core.Entities.Receipt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("DemandId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DemandId");

                    b.HasIndex("Id");

                    b.ToTable("Receipts");
                });

            modelBuilder.Entity("indiGo.Core.Entities.ReceiptDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductId1")
                        .HasColumnType("int");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ReceiptId")
                        .HasColumnType("int");

                    b.Property<int?>("ReceiptId1")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("ProductId1");

                    b.HasIndex("ReceiptId");

                    b.HasIndex("ReceiptId1");

                    b.ToTable("ReceiptDetail");
                });

            modelBuilder.Entity("indiGo.Core.Entities.ServiceDemand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Accepted")
                        .HasColumnType("bit");

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Completed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Paid")
                        .HasColumnType("bit");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Problem")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<bool>("Receipted")
                        .HasColumnType("bit");

                    b.Property<string>("ServiceId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TCKN")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nchar(11)")
                        .IsFixedLength();

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ServiceDemands");
                });

            modelBuilder.Entity("indiGo.Data.Identity.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "8e552862-a24d-4548-a6c6-9443d048cdb9",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "fcf58077-8288-4994-9c72-cab12b85b010",
                            Email = "admin@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "Admin",
                            LastName = "Admin",
                            LockoutEnabled = false,
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAEAACcQAAAAEKmvGZ67z8oIPmnIHggcUKVUdZO31/PgSGcFt/Wqgpm/7J2+3T0/pbrxmFjhLNjRXA==",
                            PhoneNumberConfirmed = false,
                            RegisterDate = new DateTime(2022, 6, 24, 1, 18, 34, 643, DateTimeKind.Utc).AddTicks(9748),
                            SecurityStamp = "589308c4-3916-4310-9c53-0dcdb98d27c8",
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        },
                        new
                        {
                            Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "fcc30e9c-1836-4239-a8dd-72a6fcaee541",
                            Email = "operator@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "Operator",
                            LastName = "Operator",
                            LockoutEnabled = false,
                            NormalizedUserName = "OPERATOR",
                            PasswordHash = "AQAAAAEAACcQAAAAEOv9s3Qm58lC+9TXXlTBhbA5YbRSd04IDqf3mvRaBIv1xs229exJiUeHXDeAvUnn3g==",
                            PhoneNumberConfirmed = false,
                            RegisterDate = new DateTime(2022, 6, 24, 1, 18, 34, 645, DateTimeKind.Utc).AddTicks(4500),
                            SecurityStamp = "a3ef2144-37eb-4185-af3c-e240b5e16b9c",
                            TwoFactorEnabled = false,
                            UserName = "operator"
                        },
                        new
                        {
                            Id = "8e443125-a24d-4543-a6c6-9443d048cdb9",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "a51288a4-dab5-4e02-a2dc-f4c01a39da48",
                            Email = "mandosi@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "Mandosi",
                            LastName = "Paki",
                            LockoutEnabled = false,
                            NormalizedUserName = "MANDOSI",
                            PasswordHash = "AQAAAAEAACcQAAAAEN7TJw+Qh7l+tCzE1B8r9PHNOAwphYsgfwmJ5X9D0NitakkMBD8IJKygNWuEEQd4nw==",
                            PhoneNumberConfirmed = false,
                            RegisterDate = new DateTime(2022, 6, 24, 1, 18, 34, 646, DateTimeKind.Utc).AddTicks(8653),
                            SecurityStamp = "3b8b9067-86ce-4568-bf23-9da5de06e976",
                            TwoFactorEnabled = false,
                            UserName = "mandosi"
                        },
                        new
                        {
                            Id = "8e443125-a24d-4543-a6c6-8223d048cdb9",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "b59eb939-e70f-41cb-8692-73279dbb9ab1",
                            Email = "bewar@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "Bewar",
                            LastName = "Dılbixhin",
                            LockoutEnabled = false,
                            NormalizedUserName = "BEWAR",
                            PasswordHash = "AQAAAAEAACcQAAAAEHcUwyLnIks8eVg1FIHhpLfMIpCmsVfgEC6GiVfWxSL0a4ztEQdK43HFpzvSiFwbgg==",
                            PhoneNumberConfirmed = false,
                            RegisterDate = new DateTime(2022, 6, 24, 1, 18, 34, 648, DateTimeKind.Utc).AddTicks(2712),
                            SecurityStamp = "30831686-c49b-43cf-8c99-25d07454f67a",
                            TwoFactorEnabled = false,
                            UserName = "bewar"
                        },
                        new
                        {
                            Id = "8e443125-a24d-4543-a5g5-8223d048cdb9",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "048119a7-1cce-4ca0-8afd-affe3c0d2093",
                            Email = "cumali@gmail.com",
                            EmailConfirmed = false,
                            FirstName = "Cumali",
                            LastName = "Cemalikızık",
                            LockoutEnabled = false,
                            NormalizedUserName = "CUMALI",
                            PasswordHash = "AQAAAAEAACcQAAAAEN1FgI4H+MBDSjRGt+Kt3uAt3gTsUvqfJ0ZqVXPJkpSRKd61liJEIhJ3t+s/jBSZBA==",
                            PhoneNumberConfirmed = false,
                            RegisterDate = new DateTime(2022, 6, 24, 1, 18, 34, 649, DateTimeKind.Utc).AddTicks(6770),
                            SecurityStamp = "97b780ed-03e8-4d7a-8760-b47870bd0836",
                            TwoFactorEnabled = false,
                            UserName = "cumali"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "1",
                            ConcurrencyStamp = "f63f9785-a69a-4788-b533-c9615b576a83",
                            Name = "ADMIN",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "2",
                            ConcurrencyStamp = "83f84006-6349-4c8f-a4ea-f6cb60bf3035",
                            Name = "CUSTOMER",
                            NormalizedName = "CUSTOMER"
                        },
                        new
                        {
                            Id = "3",
                            ConcurrencyStamp = "9b8cb7ad-c599-4a0a-820e-d80194ad3ce4",
                            Name = "OPERATOR",
                            NormalizedName = "OPERATOR"
                        },
                        new
                        {
                            Id = "4",
                            ConcurrencyStamp = "a6d1fe60-3d34-4e9f-86d4-93eac4e35480",
                            Name = "PASSIVE",
                            NormalizedName = "PASSIVE"
                        },
                        new
                        {
                            Id = "5",
                            ConcurrencyStamp = "2dcbf235-2f96-4de0-8fc8-1da21fa9bc55",
                            Name = "ELECTRICALSERVICE",
                            NormalizedName = "ELECTRICALSERVICE"
                        },
                        new
                        {
                            Id = "6",
                            ConcurrencyStamp = "ae28c361-7f3f-4204-966c-35dd1067146c",
                            Name = "GASSERVICE",
                            NormalizedName = "GASSERVICE"
                        },
                        new
                        {
                            Id = "7",
                            ConcurrencyStamp = "d10489bb-9f25-4b6b-9a93-98d831417f68",
                            Name = "PLUMBINGSERVICE",
                            NormalizedName = "PLUMBINGSERVICE"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "8e552862-a24d-4548-a6c6-9443d048cdb9",
                            RoleId = "1"
                        },
                        new
                        {
                            UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                            RoleId = "3"
                        },
                        new
                        {
                            UserId = "8e443125-a24d-4543-a6c6-9443d048cdb9",
                            RoleId = "5"
                        },
                        new
                        {
                            UserId = "8e443125-a24d-4543-a6c6-8223d048cdb9",
                            RoleId = "6"
                        },
                        new
                        {
                            UserId = "8e443125-a24d-4543-a5g5-8223d048cdb9",
                            RoleId = "7"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("indiGo.Core.Entities.Address", b =>
                {
                    b.HasOne("indiGo.Data.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("indiGo.Core.Entities.Receipt", b =>
                {
                    b.HasOne("indiGo.Core.Entities.ServiceDemand", "Demand")
                        .WithMany()
                        .HasForeignKey("DemandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Demand");
                });

            modelBuilder.Entity("indiGo.Core.Entities.ReceiptDetail", b =>
                {
                    b.HasOne("indiGo.Core.Entities.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("indiGo.Core.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId1");

                    b.HasOne("indiGo.Core.Entities.Receipt", null)
                        .WithMany()
                        .HasForeignKey("ReceiptId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("indiGo.Core.Entities.Receipt", "Receipt")
                        .WithMany()
                        .HasForeignKey("ReceiptId1");

                    b.Navigation("Product");

                    b.Navigation("Receipt");
                });

            modelBuilder.Entity("indiGo.Core.Entities.ServiceDemand", b =>
                {
                    b.HasOne("indiGo.Core.Entities.Address", null)
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("indiGo.Data.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("indiGo.Data.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("indiGo.Data.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("indiGo.Data.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("indiGo.Data.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
