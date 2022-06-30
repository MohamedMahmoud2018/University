using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DTO
{
    public class ProfileExperiencesInput
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int ProfileId { get; set; }
        public string JopName { get; set; }
        public System.DateTime? FromDate { get; set; }
        public System.DateTime? ToDate { get; set; }
        public string Description { get; set; }
       [Required]
        public int SpecialityRelationId { get; set; }
        [Required]
        public int EmployerId { get; set; }
    }
}
