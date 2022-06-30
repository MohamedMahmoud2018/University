using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DTO
{
    public class SpecialtyInput
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(40, ErrorMessage = "اقصى عدد للاحرف 40")]
        public string Name { get; set; }
    }
}
