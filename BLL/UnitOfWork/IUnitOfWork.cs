using CORE.DAL;

namespace BLL
{
    public interface IUnitOfWork
    {
        IRepository<Gender> GenderRepo { get; }
        IRepository<City> CityRepo { get; }
        IRepository<Country> CountryRepo { get; }
        IRepository<DiscoveryChannels> DiscoveryChannelsRepo { get; }
        IRepository<AcademicDegree> AcademicDegreeRepo { get; }
        IRepository<ProfileCourses> ProfileCoursesRepo { get; }
        IRepository<ProfileExperiences> ProfileExperiencesRepo { get; }
        IRepository<ProfileAcademicDegree> ProfileAcademicDegreeRepo { get;}
        IRepository<Specialty> SpecialtyRepo { get;}
                                                                       
        IRepository<profile> ProfileRepo { get; }
        IRepository<KPI> KPIRepo { get; }
        IRepository<SupKPI> SupKPIRepo { get; }
        IRepository<KPICategory> KPICategoryRepo { get; }
        IRepository<Grade> GradeRepo { get; }
        IRepository<ProfileEvaluation> ProfileEvaluationRepo { get; }

        IRepository<PositionAbbreviation> PositionAbbreviationRepo { get; }
        IRepository<University> UniversityRepo { get; }
        IRepository<AcademicRank> AcademicRankRepo { get; }
        IRepository<CertificateType> CertificateTypeRepo { get; }
        IRepository<ExperienceSpecialityRelation> ExperienceSpecialityRelationRepo { get; }
        IRepository<ProfileAwards> ProfileAwardsRepo { get; }
        IRepository<ProfileScientificResearch> ProfileScientificResearchRepo { get; }
        IRepository<ScientificResearchParticipant> ScientificResearchParticipantRepo { get; }
        void Save();
    }
}
