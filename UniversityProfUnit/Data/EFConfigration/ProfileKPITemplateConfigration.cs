using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProfUnit.Logic.ProfileKPIAgreget;

namespace UniversityProfUnit.Data.EFConfigration
{
    public class ProfileKPITemplateConfigration : IEntityTypeConfiguration<ProfileKPITemplate>
    {
        public void Configure(EntityTypeBuilder<ProfileKPITemplate> builder)
        {
            builder.ToTable("ProfileKPITemplate");
            builder.Property(x => x.ProfileKPITemplateId).UseIdentityColumn().IsRequired();
            builder.HasMany(x => x.ProfileKPIMainCategoryList).WithOne().HasForeignKey(x => x.ProfileKPITemplateId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
