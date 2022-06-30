using CORE.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DTO
{
    public class ProfileAcademicDegreeOutput
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public DateTime? DegreeStartDate { get; set; }
        public DateTime? DegreeEndDate { get; set; }
        public string SupSpecialty { get; set; }

        //public int? SecondSpecialtyId { get; set; }
        //public string SecondSpecialtyName { get; set; }
        //public string StudentActivity { get; set; }
        public virtual University University { get; set; }
        public virtual AcademicDegree AcademicDegree { get; set; }
        public virtual Specialty Specialty { get; set; }
    }
}
