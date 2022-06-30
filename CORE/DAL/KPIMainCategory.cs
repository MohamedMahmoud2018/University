using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DAL
{
    public class KPIMainCategory
    {
        public int Id { get; set; }
        public int KPITemplateId { get; set; }
        public string Name { get; set; }
        public int Wehight { get; set; }
        public virtual ICollection<KPISubCategory> KPISubCategory { get; set;}
    }
}
