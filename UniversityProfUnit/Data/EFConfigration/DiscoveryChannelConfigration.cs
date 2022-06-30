using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProfUnit.Logic;

namespace UniversityProfUnit.Data.EFConfigration
{
    public class DiscoveryChannelConfigration : IEntityTypeConfiguration<DiscoveryChannel>
    {
        public void Configure(EntityTypeBuilder<DiscoveryChannel> builder)
        {
            builder.ToTable("DiscoveryChannels");
            builder.Property(x => x.DiscoveryChannelId).UseIdentityColumn().IsRequired();
            builder.Property(p => p.DiscoveryChannelName).HasMaxLength(200);
        }
    }
}
