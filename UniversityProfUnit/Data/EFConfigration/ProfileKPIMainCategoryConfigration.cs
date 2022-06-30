using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProfUnit.Logic.ProfileKPIAgreget;

namespace UniversityProfUnit.Data.EFConfigration
{
    public class ProfileKPIMainCategoryConfigration : IEntityTypeConfiguration<ProfileKPIMainCategory>
    {
        public void Configure(EntityTypeBuilder<ProfileKPIMainCategory> builder)
        {
            builder.ToTable("ProfileKPIMainCategory");
            builder.Property(x => x.ProfileKPIMainCategoryId).UseIdentityColumn().IsRequired();
            builder.HasMany(x => x.ProfileKPISubCategoryList).WithOne().HasForeignKey(x => x.ProfileKPIMainCategoryId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
