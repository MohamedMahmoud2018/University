using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityProfUnit.Application.Profiles.Queries.GetProfileList
{
    public class GetProfileListDto
    {
        public int Serial { get; set; }
        public int ProfileSerial { get; set; }
        public string Name { get; set; }
        public int SpecialtyId { get; set; }
        public string SpecialtyName { get; set; }
        public string PositionAbbreviation { get; set; }
        public string MainJob { get; set; }
    }
}
