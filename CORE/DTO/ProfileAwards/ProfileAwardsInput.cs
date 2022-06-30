using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DTO
{
   public class ProfileAwardsInput
    {
        [Required]
        public int Id { get; set; }
        public int ProfileId { get; set; }
        [Required(ErrorMessage = "الرجاء إدخال اسم الجائزة")]
        public string AwardName { get; set; }
        [Required(ErrorMessage ="الرجاء إدخال الجهة المانحة")]
        public string Donor { get; set; }
        public DateTime? AwardDate { get; set; }
        public int? AwardValue { get; set; }
    }
}
