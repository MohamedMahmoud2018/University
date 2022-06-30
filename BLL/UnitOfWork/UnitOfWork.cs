using CORE;
using CORE.DAL;
using System;

namespace BLL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDBContext context;

        public UnitOfWork(ApplicationDBContext _context)
        {
            context = _context;
        }


        private IRepository<Gender> genderRepo;
        public IRepository<Gender> GenderRepo
        {
            get { return genderRepo ?? (genderRepo = new Repository<Gender>(context)); }
        }

        private IRepository<Country> countryRepo;
        public IRepository<Country> CountryRepo
        {
            get { return countryRepo ?? (countryRepo = new Repository<Country>(context)); }
        }

        private IRepository<City> cityRepo;
        public IRepository<City> CityRepo
        {
            get { return cityRepo ?? (cityRepo = new Repository<City>(context)); }
        }

        private IRepository<DiscoveryChannels> discoveryChannelsRepo;
        public IRepository<DiscoveryChannels> DiscoveryChannelsRepo
        {
            get { return discoveryChannelsRepo ?? (discoveryChannelsRepo = new Repository<DiscoveryChannels>(context)); }
        }

        private IRepository<AcademicDegree> academicDegreeRepo;
        public IRepository<AcademicDegree> AcademicDegreeRepo
        {
            get { return academicDegreeRepo ?? (academicDegreeRepo = new Repository<AcademicDegree>(context)); }
        }

        private IRepository<ProfileCourses> profileCoursesRepo;
        public IRepository<ProfileCourses> ProfileCoursesRepo
        {
            get { return profileCoursesRepo ?? (profileCoursesRepo = new Repository<ProfileCourses>(context)); }
        }

        private IRepository<ProfileExperiences> profileExperiencesRepo;
        public IRepository<ProfileExperiences> ProfileExperiencesRepo
        {
            get { return profileExperiencesRepo ?? (profileExperiencesRepo = new Repository<ProfileExperiences>(context)); }
        }

        private IRepository<ProfileAcademicDegree> profileAcademicDegreeRepo;
        public IRepository<ProfileAcademicDegree> ProfileAcademicDegreeRepo
        {
            get { return profileAcademicDegreeRepo ?? (profileAcademicDegreeRepo = new Repository<ProfileAcademicDegree>(context)); }
        }

        private IRepository<Specialty> specialtyRepo;
        public IRepository<Specialty> SpecialtyRepo
        {
            get { return specialtyRepo ?? (specialtyRepo = new Repository<Specialty>(context)); }
        }

       

        private IRepository<profile> profileRepo;
        public IRepository<profile> ProfileRepo
        {
            get { return profileRepo ?? (profileRepo = new Repository<profile>(context)); }
        }

        private IRepository<KPI> kPIRepo;
        public IRepository<KPI> KPIRepo
        {
            get { return kPIRepo ?? (kPIRepo = new Repository<KPI>(context)); }
        }

        private IRepository<SupKPI> SupkPIRepo;
        public IRepository<SupKPI> SupKPIRepo
        {
            get { return SupkPIRepo ?? (SupkPIRepo = new Repository<SupKPI>(context)); }
        }

        private IRepository<KPICategory> kPICategoryRepo;
        public IRepository<KPICategory> KPICategoryRepo
        {
            get { return kPICategoryRepo ?? (kPICategoryRepo = new Repository<KPICategory>(context)); }
        }
      
        private IRepository<Grade> gradeRepo;
        public IRepository<Grade> GradeRepo
        {
            get { return gradeRepo ?? (gradeRepo = new Repository<Grade>(context)); }
        }

        private IRepository<ProfileEvaluation> profileEvaluationRepo;
        public IRepository<ProfileEvaluation> ProfileEvaluationRepo
        {
            get { return profileEvaluationRepo ?? (profileEvaluationRepo = new Repository<ProfileEvaluation>(context)); }
        }

        private IRepository<PositionAbbreviation> positionAbbreviationRepo;
        public IRepository<PositionAbbreviation> PositionAbbreviationRepo
        {
            get { return positionAbbreviationRepo ?? (positionAbbreviationRepo = new Repository<PositionAbbreviation>(context)); }
        }

        private IRepository<AcademicRank> academicRankRepo;
        public IRepository<AcademicRank> AcademicRankRepo
        {
            get { return academicRankRepo ?? (academicRankRepo = new Repository<AcademicRank>(context)); }
        }

        private IRepository<University> universityRepo;
        public IRepository<University> UniversityRepo
        {
            get { return universityRepo ?? (universityRepo = new Repository<University>(context)); }
        }

        private IRepository<CertificateType> certificateTypeRepo;
        public IRepository<CertificateType> CertificateTypeRepo
        {
            get { return certificateTypeRepo ?? (certificateTypeRepo = new Repository<CertificateType>(context)); }
        }

        private IRepository<ExperienceSpecialityRelation> experienceSpecialityRelationRepo;
        public IRepository<ExperienceSpecialityRelation> ExperienceSpecialityRelationRepo
        {
            get { return experienceSpecialityRelationRepo ?? (experienceSpecialityRelationRepo = new Repository<ExperienceSpecialityRelation>(context)); }
        }

        private IRepository<ProfileAwards> profileAwardsRepo;
        public IRepository<ProfileAwards> ProfileAwardsRepo
        {
            get { return profileAwardsRepo ?? (profileAwardsRepo = new Repository<ProfileAwards>(context)); }
        }

        private IRepository<ProfileScientificResearch> profileScientificResearchRepo;
        public IRepository<ProfileScientificResearch> ProfileScientificResearchRepo
        {
            get { return profileScientificResearchRepo ?? (profileScientificResearchRepo = new Repository<ProfileScientificResearch>(context)); }
        }

        private IRepository<ScientificResearchParticipant> scientificResearchParticipantRepo;
        public IRepository<ScientificResearchParticipant> ScientificResearchParticipantRepo
        {
            get { return scientificResearchParticipantRepo ?? (scientificResearchParticipantRepo = new Repository<ScientificResearchParticipant>(context)); }
        }

        public void Save()
        {
            context.SaveChanges();
        }
        public void Dispose()
        {
            context.Dispose();
            System.GC.SuppressFinalize(this);
        }
    }
}
