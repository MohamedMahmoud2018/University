using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProfUnit.Logic.KPIAgreget;

namespace UniversityProfUnit.Data.EFConfigration
{
    public class KPITemplateConfigration : IEntityTypeConfiguration<KPITemplate>
    {
        public void Configure(EntityTypeBuilder<KPITemplate> builder)
        {
            builder.ToTable("KPITemplate");
            builder.Property(x => x.KPITemplateId).UseIdentityColumn().IsRequired();
            builder.Property(p => p.KPITemplateName).HasMaxLength(200);
            builder.HasMany(x => x.KPIMainCategoryList).WithOne().HasForeignKey(x => x.KPITemplateId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
