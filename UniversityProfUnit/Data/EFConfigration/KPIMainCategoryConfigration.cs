using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProfUnit.Logic.KPIAgreget;

namespace UniversityProfUnit.Data.EFConfigration
{
    public class KPIMainCategoryConfigration : IEntityTypeConfiguration<KPIMainCategory>
    {
        public void Configure(EntityTypeBuilder<KPIMainCategory> builder)
        {
            builder.ToTable("KPIMainCategory");
            builder.Property(x => x.KPIMainCategoryId).UseIdentityColumn().IsRequired();
            builder.Property(p => p.KPIMainCategoryName).HasMaxLength(200);
            builder.HasMany(x => x.KPISupCategoryList).WithOne().HasForeignKey(x => x.KPIMainCategoryId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
