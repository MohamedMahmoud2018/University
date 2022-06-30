using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DTO
{
    public class ProfileCoursesInput
    {
        [Required]
        public int Id { get; set; }
        public int ProfileId { get; set; }
        [Required]
        public string CourseName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Description { get; set; }
        [Required]
        public string Donor { get; set; }
        [Required]
        public int CertificateTypeId { get; set; }
        [Required]
        public int CityId { get; set; }
    }
}
