using AutoMapper;
using CORE.DAL;
using CORE.DTO;

namespace BLL
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Gender, GenderInput>().ReverseMap();
            CreateMap<Gender, GenderOutput>();

            CreateMap<City, CityInput>().ReverseMap();
            CreateMap<City, CityOutput>();

            CreateMap<Country, CountryInput>().ReverseMap();
            CreateMap<Country, CountryOutput>();

            CreateMap<DiscoveryChannels, DiscoveryChannelsInput>().ReverseMap();
            CreateMap<DiscoveryChannels, DiscoveryChannelsOutput>();

            CreateMap<AcademicDegree, AcademicDegreeInput>().ReverseMap();
            CreateMap<AcademicDegree, AcademicDegreeOutput>();

            CreateMap<AcademicRank, AcademicRankInput>().ReverseMap();
            CreateMap<AcademicRank, AcademicRankOutput>();

            CreateMap<ProfileCourses, ProfileCoursesInput>().ReverseMap();
            CreateMap<ProfileCourses, ProfileCoursesOutput>();

            CreateMap<ProfileExperiences, ProfileExperiencesInput>().ReverseMap();
            CreateMap<ProfileExperiences, ProfileExperiencesOutput>();

            CreateMap<ProfileAcademicDegree, ProfileAcademicDegreeInput>().ReverseMap();
            CreateMap<ProfileAcademicDegree, ProfileAcademicDegreeOutput>();

            CreateMap<Specialty, SpecialtyInput>().ReverseMap();
            CreateMap<Specialty, SpecialtyOutput>();

            CreateMap<Grade, GradeInput>().ReverseMap();
            CreateMap<Grade, GradeOutput>();

            CreateMap<KPI, KPIInput>().ReverseMap();
            CreateMap<KPI, KPIOutput>();

            CreateMap<SupKPI, SupKPIInput>().ReverseMap();
            CreateMap<SupKPI, SupKPIOutput>();

            CreateMap<KPICategory, KPICategoryInput>().ReverseMap();
            CreateMap<KPICategory, KPICategoryOutput>();

            CreateMap<ProfileEvaluation, ProfileEvaluationInput>().ReverseMap();
            CreateMap<ProfileEvaluation, ProfileEvaluationOutput>();

            CreateMap<profile, ProfileInput>().ReverseMap();
            CreateMap<profile, ProfileOutput>();

            CreateMap<PositionAbbreviation, PositionAbbreviationInput>().ReverseMap();
            CreateMap<PositionAbbreviation, PositionAbbreviationOutput>();

            CreateMap<University, UniversityInput>().ReverseMap();
            CreateMap<University, UniversityOutput>();

            CreateMap<ProfileScientificResearch, ProfileScientificResearchInput>().ReverseMap();
            CreateMap<ProfileScientificResearch, ProfileScientificResearchOutput>();

            CreateMap<ScientificResearchParticipant, ScientificResearchParticipantInput>().ReverseMap();
            CreateMap<ScientificResearchParticipant, ScientificResearchParticipantOutput>();

            CreateMap<ProfileAwards, ProfileAwardsInput>().ReverseMap();
            CreateMap<ProfileAwards, ProfileAwardsOutput>();

            CreateMap<CertificateType, CertificateTypeInput>().ReverseMap();
            CreateMap<CertificateType, CertificateTypeOutput>();

            CreateMap<ExperienceSpecialityRelation, ExperienceSpecialityRelationInput>().ReverseMap();
            CreateMap<ExperienceSpecialityRelation, ExperienceSpecialityRelationOutput>();
        }
    }
}
