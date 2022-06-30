using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProfUnit.Logic.KPIAgreget;

namespace UniversityProfUnit.Data.EFConfigration
{
    public class KPISupCategoryConfigration : IEntityTypeConfiguration<KPISupCategory>
    {
        public void Configure(EntityTypeBuilder<KPISupCategory> builder)
        {
            builder.ToTable("KPISupCategory");
            builder.Property(x => x.KPISupCategoryId).UseIdentityColumn().IsRequired();
            builder.Property(p => p.KPISupCategoryName).HasMaxLength(200);
        }
    }
}
