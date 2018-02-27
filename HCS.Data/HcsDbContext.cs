using HCS.Core.Domain;
using HCS.Data.EntityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HCS.Data
{
    public class HcsDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Location> Locations { get; set; }
        public DbSet<Consumer> Consumers { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Household> Households { get; set; }
        public DbSet<Occupant> Occupants { get; set; }
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
