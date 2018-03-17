using HCS.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCS.Data.EntityConfigurations
{
    public class OccupantConfiguration : IEntityTypeConfiguration<Occupant>
    {
        public void Configure(EntityTypeBuilder<Occupant> builder)
        {
            builder.HasOne(x => x.Household).WithMany(y => y.Occupants)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
