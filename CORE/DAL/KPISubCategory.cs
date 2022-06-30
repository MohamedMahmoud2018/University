using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DAL
{
    public class KPISubCategory
    {
        public int Id { get; set; }
        public int KPIMainCategoryId { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
    }
}
