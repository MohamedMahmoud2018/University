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
    public class ProfileCourseConfigration : IEntityTypeConfiguration<ProfileCourse>
    {
        public void Configure(EntityTypeBuilder<ProfileCourse> builder)
        {
            builder.ToTable("ProfileCourses");
            builder.Property(x => x.ProfileCourseId).UseIdentityColumn().IsRequired();
            builder.Property(p => p.CourseName).HasMaxLength(200);
            builder.Property(p => p.Description).HasMaxLength(500);
        }
    }
}
