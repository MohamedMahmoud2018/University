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
    public class ProfileExperienceConfigration : IEntityTypeConfiguration<ProfileExperience>
    {
        public void Configure(EntityTypeBuilder<ProfileExperience> builder)
        {
            builder.ToTable("ProfileExperiences");
            builder.Property(x => x.ProfileExperienceId).UseIdentityColumn().IsRequired();
            builder.Property(p => p.JopName).HasMaxLength(200);
            builder.Property(p => p.Employer).HasMaxLength(200);
            builder.Property(p => p.Description).HasMaxLength(500);
        }
    }
}
