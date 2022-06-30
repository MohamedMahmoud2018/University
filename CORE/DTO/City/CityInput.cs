using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DTO
{
   public class CityInput
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ArabicName { get; set; }
        [Required]
        public int CountryId { get; set; }
    }
}
