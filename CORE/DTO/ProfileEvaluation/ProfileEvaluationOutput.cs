using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DTO
{
    public class ProfileEvaluationOutput
    {
        public int Id { get; set; }
        public int PofileId { get; set; }
        public int KPICategoryId { get; set; }
        public int KPIId { get; set; }
        public int SupKPIId { get; set; }
        public int KPIDegree { get; set; }
        public int SupKPIDegree { get; set; }
        public int Grade { get; set; }
        public string Notes { get; set; }
    }
}
