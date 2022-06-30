using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProfUnit.Common.Mapping;
using UniversityProfUnit.Logic.ProfileAgreget;

namespace UniversityProfUnit.Application.Profiles.Queries.GetProfileById
{
    public class GetProfileByProfileIdDto : IMapFrom<Profile>
    {
        public int ProfileId { get; set; }
        public int Serial { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int SpecialtyId { get; set; }
        public string PositionAbbreviation { get; set; }
        public string CurrentJop { get; set; }
        public string MainJob { get; set; }
        public int GenderId { get; set; }
        public int? DiscoveryChannelId { get; set; }
        public string Description { get; set; }
        public bool ProfileState { get; set; }
        public List<GetProfileAcademicDegreeByProfileIdDto> ProfileAcademicDegreeList { get; set; } = new List<GetProfileAcademicDegreeByProfileIdDto>();
        public List<GetProfileCourseByProfileIdDto> ProfileCourseList { get; set; } = new List<GetProfileCourseByProfileIdDto>();
        public List<GetProfileExperienceByProfileIdDto> ProfileExperienceList { get; set; } = new List<GetProfileExperienceByProfileIdDto>();

        public void Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<Profile, GetProfileByProfileIdDto>()
                .ForMember(dest => dest.ProfileAcademicDegreeList, opt => opt.MapFrom(src => src.ProfileAcademicDegreeList))
                .ForMember(dest => dest.ProfileExperienceList, opt => opt.MapFrom(src => src.ProfileExperienceList))
                .ForMember(dest => dest.ProfileCourseList, opt => opt.MapFrom(src => src.ProfileCourseList));
        }
    }
}
