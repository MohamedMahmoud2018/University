using CORE.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DTO
{
    public class ProfileExperiencesOutput
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public string JopName { get; set; }
        public System.DateTime? FromDate { get; set; }
        public System.DateTime? ToDate { get; set; }
        public string Description { get; set; }
        public virtual ExperienceSpecialityRelation SpecialityRelation { get; set; }
        public virtual University Employer { get; set; }
        public int EmployerId { get; set; }
       

    }
}
