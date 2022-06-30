using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProfUnit.Logic.ProfileKPIAgreget;

namespace UniversityProfUnit.Data.EFConfigration
{
    public class ProfileKPISubCategoryConfigration : IEntityTypeConfiguration<ProfileKPISubCategory>
    {
        public void Configure(EntityTypeBuilder<ProfileKPISubCategory> builder)
        {
            builder.ToTable("ProfileKPISubCategory");
            builder.Property(x => x.ProfileKPISubCategoryId).UseIdentityColumn().IsRequired();
        }
    }
}
