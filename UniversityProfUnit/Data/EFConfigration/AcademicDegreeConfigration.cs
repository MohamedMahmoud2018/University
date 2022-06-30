using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProfUnit.Logic;

namespace UniversityProfUnit.Data.EFConfigration
{
    public class AcademicDegreeConfigration : IEntityTypeConfiguration<AcademicDegree>
    {
        public void Configure(EntityTypeBuilder<AcademicDegree> builder)
        {
            builder.ToTable("AcademicDegree");
            builder.Property(x => x.AcademicDegreeId).UseIdentityColumn().IsRequired();
            builder.Property(p => p.AcademicDegreeName).HasMaxLength(200);
        }
    }
}
