using CORE.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DTO
{
    public class ProfileCoursesOutput
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public string CourseName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Description { get; set; }
        public string Donor { get; set; }
        public int CityId { get; set; }
        public virtual CertificateType CertificateType { get; set; }
        public virtual City City { get; set; }
    }
}
