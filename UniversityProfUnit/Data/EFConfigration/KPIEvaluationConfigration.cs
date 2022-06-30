using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProfUnit.Logic.KPIAgreget;

namespace UniversityProfUnit.Data.EFConfigration
{
    public class KPIEvaluationConfigration : IEntityTypeConfiguration<KPIEvaluation>
    {
        public void Configure(EntityTypeBuilder<KPIEvaluation> builder)
        {
            builder.ToTable("KPIEvaluation");
            builder.Property(x => x.KPIEvaluationId).UseIdentityColumn().IsRequired();
            builder.Property(p => p.KPIEvaluationName).HasMaxLength(200);
        }
    }
}
