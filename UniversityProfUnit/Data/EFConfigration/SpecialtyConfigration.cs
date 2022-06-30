using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProfUnit.Logic;

namespace UniversityProfUnit.Data.EFConfigration
{
    public class SpecialtyConfigration : IEntityTypeConfiguration<Specialty>
    {
        public void Configure(EntityTypeBuilder<Specialty> builder)
        {
            builder.ToTable("Specialty");
            builder.Property(x => x.SpecialtyId).UseIdentityColumn().IsRequired();
            builder.Property(p => p.SpecialtyName).HasMaxLength(200);
        }
    }
}
