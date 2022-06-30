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
    public class ProfileConfigration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.ToTable("Profile");
            builder.Property(x => x.ProfileId).UseIdentityColumn().IsRequired();
            builder.Property(p => p.FirstName).HasMaxLength(200);
            builder.Property(p => p.SecondName).HasMaxLength(200);
            builder.Property(p => p.Email).HasMaxLength(200);
            builder.Property(p => p.PhoneNumber).HasMaxLength(200);
            builder.Property(p => p.PositionAbbreviation).HasMaxLength(200);
            builder.Property(p => p.CurrentJop).HasMaxLength(200);
            builder.Property(p => p.MainJob).HasMaxLength(200);
            builder.Property(p => p.Description).HasMaxLength(500);
            builder.HasMany(x => x.ProfileCourseList).WithOne().HasForeignKey(x => x.ProfileId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.ProfileAcademicDegreeList).WithOne().HasForeignKey(x => x.ProfileId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.ProfileExperienceList).WithOne().HasForeignKey(x => x.ProfileId).OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(p => p.Email).IsUnique();
            builder.HasIndex(p => p.PhoneNumber).IsUnique();
        }
    }
}
