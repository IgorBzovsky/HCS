using HCS.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCS.Data.EntityConfigurations
{
    public class ConsumerConfiguration : IEntityTypeConfiguration<Consumer>
    {
        public void Configure(EntityTypeBuilder<Consumer> builder)
        {
            //builder.HasOne(x => x.Location)
            //    .WithOne(x => x.Consumer)
            //    .HasForeignKey<Location>(l => l.Id);
        }
    }
}
