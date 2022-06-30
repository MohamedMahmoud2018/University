using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProfUnit.Common.Mapping;

namespace UniversityProfUnit.Application.Profiles.Queries.GetProfileById
{
    public class GetProfileAcademicDegreeByProfileIdDto : IMapFrom<Logic.AcademicDegree>
    {
        public int ProfileAcademicDegreeId { get; set; }
        public int AcademicDegreeId { get; set; }
        public string Specialty { get; set; }
        public string University { get; set; }
        public DateTime DegreeDate { get; set; }

        public void Mapping(AutoMapper.Profile profile)
        {
            profile.CreateMap<Logic.ProfileAgreget.ProfileAcademicDegree, GetProfileAcademicDegreeByProfileIdDto>();
        }
    }
}
