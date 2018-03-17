using HCS.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCS.Data.EntityConfigurations
{
    public class HouseholdConfiguration : IEntityTypeConfiguration<Household>
    {
        public void Configure(EntityTypeBuilder<Household> builder)
        {
            builder.HasMany(x => x.Occupants)
                .WithOne(y => y.Household)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
