using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DTO
{
    public class ProfileAcademicDegreeInput
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public int AcademicDegreeId { get; set; }
        public int SpecialtyId { get; set; }
        [Required]
        public int UniversityId { get; set; }
        public DateTime? DegreeStartDate { get; set; }
        public DateTime? DegreeEndDate { get; set; }
        //[NotMapped]
        //public int? SecondSpecialtyId { get; set; }
        //public string StudentActivity { get; set; }
        public string SupSpecialty { get; set; }
    }
}
