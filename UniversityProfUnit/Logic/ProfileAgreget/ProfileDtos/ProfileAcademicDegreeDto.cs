using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityProfUnit.Logic.ProfileAgreget.ProfileDtos
{
    public class ProfileAcademicDegreeDto
    {
        public int ProfileAcademicDegreeId { get; set; }
        public Maybe<AcademicDegree> AcademicDegree { get; set; }
        public string Specialty { get; set; }
        public string University { get; set; }
        public DateTime DegreeDate { get; set; }
    }
}
