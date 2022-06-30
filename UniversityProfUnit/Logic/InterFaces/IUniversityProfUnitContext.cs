using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniversityProfUnit.Logic.ProfileAgreget;
using UniversityProfUnit.Logic.KPIAgreget;
using UniversityProfUnit.Logic.ProfileKPIAgreget;

namespace UniversityProfUnit.Logic.InterFaces
{
    public interface IUniversityProfUnitContext
    {
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<DiscoveryChannel> DiscoveryChannels { get; set; }
        public DbSet<AcademicDegree> AcademicDegrees { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<KPITemplate> KPITemplates { get; set; }
        public DbSet<KPIEvaluation> KPIEvaluations { get; set; }
        public DbSet<ProfileKPITemplate> ProfileKPITemplates { get; set; }

        Task<Result> SaveChangesWithValidation(CancellationToken cancellationToken = default(CancellationToken));
    }
}
