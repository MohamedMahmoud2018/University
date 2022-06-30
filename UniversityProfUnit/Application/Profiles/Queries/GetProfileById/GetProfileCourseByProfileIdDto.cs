using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProfUnit.Common.Mapping;
using UniversityProfUnit.Logic.ProfileAgreget;

namespace UniversityProfUnit.Application.Profiles.Queries.GetProfileById
{
    public class GetProfileCourseByProfileIdDto : IMapFrom<ProfileCourse>
    {
		public int ProfileCourseId { get; set; }
		public string CourseName { get; set; }
		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }
		public string Description { get; set; }

        public void Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<ProfileCourse, GetProfileCourseByProfileIdDto>();
        }
    }
}
