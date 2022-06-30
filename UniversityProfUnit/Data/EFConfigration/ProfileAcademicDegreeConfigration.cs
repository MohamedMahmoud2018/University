using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProfUnit.Logic;
using UniversityProfUnit.Logic.ProfileAgreget;

namespace UniversityProfUnit.Data.EFConfigration
{
    public class ProfileAcademicDegreeConfigration : IEntityTypeConfiguration<ProfileAcademicDegree>
    {
        public void Configure(EntityTypeBuilder<ProfileAcademicDegree> builder)
        {
            builder.ToTable("ProfileAcademicDegree");
            builder.Property(x => x.ProfileAcademicDegreeId).UseIdentityColumn().IsRequired();
            builder.Property(p => p.Specialty).HasMaxLength(200);
            builder.Property(p => p.University).HasMaxLength(200);
            builder.HasOne(p => p.AcademicDegree).WithMany().HasForeignKey(x => x.AcademicDegreeId);
        }
    }
}
