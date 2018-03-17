﻿// <auto-generated />
using HCS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace HCS.Data.Migrations
{
    [DbContext(typeof(HcsDbContext))]
    [Migration("20180314193224_Add IsDeleted to ApplicationUser")]
    partial class AddIsDeletedtoApplicationUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HCS.Core.Domain.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("MiddleName");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<int?>("ProviderId");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("ProviderId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("HCS.Core.Domain.ConsumedUtility", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ConsumerId");

                    b.Property<decimal?>("ObligatoryPrice");

                    b.Property<int>("ProvidedUtilityId");

                    b.HasKey("Id");

                    b.HasIndex("ConsumerId");

                    b.HasIndex("ProvidedUtilityId");

                    b.ToTable("ConsumedUtilities");
                });

            modelBuilder.Entity("HCS.Core.Domain.Consumer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<double>("Area");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int>("LocationId");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("LocationId");

                    b.ToTable("Consumers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Consumer");
                });

            modelBuilder.Entity("HCS.Core.Domain.ConsumptionNorm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Amount");

                    b.Property<int>("ConsumedUtilityId");

                    b.Property<int>("OccupantId");

                    b.HasKey("Id");

                    b.HasIndex("ConsumedUtilityId");

                    b.HasIndex("OccupantId");

                    b.ToTable("ConsumptionNorms");
                });

            modelBuilder.Entity("HCS.Core.Domain.Exemption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("Percent");

                    b.HasKey("Id");

                    b.ToTable("Exemptions");
                });

            modelBuilder.Entity("HCS.Core.Domain.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Appartment");

                    b.Property<string>("Building");

                    b.Property<string>("Name");

                    b.Property<int?>("ParentId");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("HCS.Core.Domain.Occupant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ExemptionId");

                    b.Property<string>("FirstName");

                    b.Property<int>("HouseholdId");

                    b.Property<string>("LastName");

                    b.Property<string>("MiddleName");

                    b.HasKey("Id");

                    b.HasIndex("ExemptionId");

                    b.HasIndex("HouseholdId");

                    b.ToTable("Occupants");
                });

            modelBuilder.Entity("HCS.Core.Domain.ProvidedUtility", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ProviderId");

                    b.Property<int>("UtilityId");

                    b.HasKey("Id");

                    b.HasIndex("ProviderId");

                    b.HasIndex("UtilityId");

                    b.ToTable("ProvidedUtilities");
                });

            modelBuilder.Entity("HCS.Core.Domain.Provider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("LocationId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Providers");
                });

            modelBuilder.Entity("HCS.Core.Domain.Utility", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsSeasonal");

                    b.Property<string>("MeasureUnit");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Utilities");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("HCS.Core.Domain.Household", b =>
                {
                    b.HasBaseType("HCS.Core.Domain.Consumer");

                    b.Property<bool>("HasCentralGasSupply");

                    b.Property<bool>("HasElectricHeating");

                    b.Property<bool>("HasElectricHotplates");

                    b.Property<bool>("HasSubsidy");

                    b.Property<bool>("HasTowelRail");

                    b.ToTable("Household");

                    b.HasDiscriminator().HasValue("Household");
                });

            modelBuilder.Entity("HCS.Core.Domain.Organization", b =>
                {
                    b.HasBaseType("HCS.Core.Domain.Consumer");


                    b.ToTable("Organization");

                    b.HasDiscriminator().HasValue("Organization");
                });

            modelBuilder.Entity("HCS.Core.Domain.ApplicationUser", b =>
                {
                    b.HasOne("HCS.Core.Domain.Provider", "Provider")
                        .WithMany("ApplicationUsers")
                        .HasForeignKey("ProviderId");
                });

            modelBuilder.Entity("HCS.Core.Domain.ConsumedUtility", b =>
                {
                    b.HasOne("HCS.Core.Domain.Consumer", "Consumer")
                        .WithMany("ConsumedUtilities")
                        .HasForeignKey("ConsumerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HCS.Core.Domain.ProvidedUtility", "ProvidedUtility")
                        .WithMany()
                        .HasForeignKey("ProvidedUtilityId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("HCS.Core.Domain.Consumer", b =>
                {
                    b.HasOne("HCS.Core.Domain.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("HCS.Core.Domain.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HCS.Core.Domain.ConsumptionNorm", b =>
                {
                    b.HasOne("HCS.Core.Domain.ConsumedUtility", "ConsumedUtility")
                        .WithMany()
                        .HasForeignKey("ConsumedUtilityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HCS.Core.Domain.Occupant")
                        .WithMany("ConsumptionNorms")
                        .HasForeignKey("OccupantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HCS.Core.Domain.Location", b =>
                {
                    b.HasOne("HCS.Core.Domain.Location", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("HCS.Core.Domain.Occupant", b =>
                {
                    b.HasOne("HCS.Core.Domain.Exemption", "Exemption")
                        .WithMany()
                        .HasForeignKey("ExemptionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HCS.Core.Domain.Household", "Household")
                        .WithMany("Occupants")
                        .HasForeignKey("HouseholdId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("HCS.Core.Domain.ProvidedUtility", b =>
                {
                    b.HasOne("HCS.Core.Domain.Provider", "Provider")
                        .WithMany("ProvidedUtilities")
                        .HasForeignKey("ProviderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HCS.Core.Domain.Utility", "Utility")
                        .WithMany()
                        .HasForeignKey("UtilityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HCS.Core.Domain.Provider", b =>
                {
                    b.HasOne("HCS.Core.Domain.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("HCS.Core.Domain.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("HCS.Core.Domain.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HCS.Core.Domain.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("HCS.Core.Domain.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
