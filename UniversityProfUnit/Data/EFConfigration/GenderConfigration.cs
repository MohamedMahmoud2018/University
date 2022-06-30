using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProfUnit.Logic;

namespace UniversityProfUnit.Data.EFConfigration
{
    public class GenderConfigration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.ToTable("Gender");
            builder.Property(x => x.GenderId).UseIdentityColumn().IsRequired();
            builder.Property(p => p.GenderName).HasMaxLength(200);
        }
    }
}
