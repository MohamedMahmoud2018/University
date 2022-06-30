using CORE.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.DTO
{
    public class KPIOutput
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Wehight { get; set; }
        public int KPICategoryId { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<SupKPIOutput> SupKPI { get; set; }

    }
}
