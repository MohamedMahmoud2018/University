using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DTO
{
    public class GradeInput
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public int Degree { get; set; }
    }
}
