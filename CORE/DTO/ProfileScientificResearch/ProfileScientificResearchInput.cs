using CORE.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DTO
{
   public class ProfileScientificResearchInput
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int ProfileId { get; set; }
        [Required(ErrorMessage ="الرجاء إدخال اسم البحث")]
        public string ResearchTitle { get; set; }
        public string Journal { get; set; }
        public DateTime? PublishingDate { get; set; }
        public string JournalImpactFactor { get; set; }
        public string ParticepantsNames { get; set; }
        public virtual ICollection<ScientificResearchParticipant> ScientificResearchParticipant { get; set; }

    }
}
