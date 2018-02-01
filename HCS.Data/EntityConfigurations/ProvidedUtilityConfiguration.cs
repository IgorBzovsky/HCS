using HCS.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HCS.Data.EntityConfigurations
{
    public class ProvidedUtilityConfiguration : IEntityTypeConfiguration<ProvidedUtility>
    {
        public void Configure(EntityTypeBuilder<ProvidedUtility> builder)
        {
            builder.ToTable("ProvidedUtilities");
            builder.HasKey(k => new { k.ProviderId, k.UtilityId });
        }
    }
}
