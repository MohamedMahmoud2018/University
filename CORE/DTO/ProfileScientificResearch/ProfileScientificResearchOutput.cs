using CORE.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DTO
{
   public class ProfileScientificResearchOutput
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public string ResearchTitle { get; set; }
        public string Journal { get; set; }
        public DateTime PublishingDate { get; set; }
        public string JournalImpactFactor { get; set; }
        public string ParticepantsNames { get; set; }
        public virtual ICollection<ScientificResearchParticipantOutput> ScientificResearchParticipant { get; set; }

    }
}
