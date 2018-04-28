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
        public DbSet<Occupant> Occupants { get; set; }
        public DbSet<Utility> Utilities { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<ProvidedUtility> ProvidedUtilities { get; set; }
        public DbSet<ConsumedUtility> ConsumedUtilities { get; set; }
        public DbSet<ConsumptionNorm> ConsumptionNorms { get; set; }
        public DbSet<Exemption> Exemptions { get; set; }
        public DbSet<ConsumerCategory> ConsumerCategories { get; set; }
        public DbSet<ConsumerType> ConsumerTypes { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<UtilityBill> UtilityBills { get; set; }
        public DbSet<UtilityBillLine> UtilityBillLines { get; set; }

        public HcsDbContext(DbContextOptions<HcsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ConsumedUtilityConfiguration());
            modelBuilder.ApplyConfiguration(new OccupantConfiguration());
            modelBuilder.ApplyConfiguration(new UtilityBillConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
