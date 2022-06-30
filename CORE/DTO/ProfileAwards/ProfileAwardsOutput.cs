using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DTO
{
   public class ProfileAwardsOutput
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public string AwardName { get; set; }
        public string Donor { get; set; }
        public DateTime? AwardDate { get; set; }
        public int? AwardValue { get; set; }
    }
}
