using HCS.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCS.Data.EntityConfigurations
{
    public class ConsumedUtilityConfiguration : IEntityTypeConfiguration<ConsumedUtility>
    {
        public void Configure(EntityTypeBuilder<ConsumedUtility> builder)
        {
            builder.HasOne(x => x.ProvidedUtility)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Tariff)
                .WithMany(c => c.ConsumedUtilities)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
