using HCS.Core.Domain;
using HCS.Data.EntityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HCS.Data
{
    public class HcsDbContext : IdentityDbContext<User>
    {
        public DbSet<Location> Locations { get; set; }
        public DbSet<Utility> Utilities { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<MeasureUnit> MeasureUnits { get; set; }

        public HcsDbContext(DbContextOptions<HcsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ProvidedUtilityConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
