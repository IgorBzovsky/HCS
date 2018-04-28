using HCS.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCS.Data.EntityConfigurations
{
    public class UtilityBillConfiguration : IEntityTypeConfiguration<UtilityBill>
    {
        public void Configure(EntityTypeBuilder<UtilityBill> builder)
        {
            builder.HasOne(u => u.Consumer)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
