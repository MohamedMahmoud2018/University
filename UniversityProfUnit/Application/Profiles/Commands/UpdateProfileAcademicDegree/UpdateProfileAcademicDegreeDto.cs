using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityProfUnit.Application.Profiles.Commands.UpdateProfileAcademicDegree
{
    public class UpdateProfileAcademicDegreeDto
    {
        public int ProfileAcademicDegreeId { get; set; }
        public int AcademicDegreeId { get; set; }
        public string Specialty { get; set; }
        public string University { get; set; }
        public DateTime DegreeDate { get; set; }
    }
}
