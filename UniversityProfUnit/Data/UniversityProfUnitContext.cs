using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using UniversityProfUnit.Logic;
using UniversityProfUnit.Logic.InterFaces;
using UniversityProfUnit.Logic.KPIAgreget;
using UniversityProfUnit.Logic.ProfileAgreget;
using UniversityProfUnit.Logic.ProfileKPIAgreget;

namespace UniversityProfUnit.Data
{
    public class UniversityProfUnitContext : DbContext, IUniversityProfUnitContext
    {
        public DbSet<Gender> Genders { get ; set ; }
        public DbSet<Specialty> Specialties { get ; set ; }
        public DbSet<DiscoveryChannel> DiscoveryChannels { get; set; }
        public DbSet<AcademicDegree> AcademicDegrees { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<KPITemplate> KPITemplates { get; set; }
        public DbSet<KPIEvaluation> KPIEvaluations { get; set; }
        public DbSet<ProfileKPITemplate> ProfileKPITemplates { get; set; }

        public async Task<Result> SaveChangesWithValidation(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await base.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
            catch (DbUpdateException dbExce)
            {
                return Result.Failure(dbExce.Message);
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }

        }

        public static readonly ILoggerFactory ConsolLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
            builder.AddFilter(DbLoggerCategory.Database.Transaction.Name, LogLevel.Debug);
            builder.AddFilter(DbLoggerCategory.Database.Connection.Name, LogLevel.Information);
        });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(ConsolLoggerFactory)
                .EnableSensitiveDataLogging()
                .UseSqlServer(Program.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        //public override int SaveChanges()
        //{
        //    ChangeTracker.DetectChanges();

        //    return base.SaveChanges();
        //}
    }
}
