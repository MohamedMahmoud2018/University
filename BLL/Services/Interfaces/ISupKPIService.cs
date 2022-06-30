using CORE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
   public interface ISupKPIService
    {
        ServiceResponse Add(SupKPIInput input);
        ServiceResponse AddCollection(ICollection<SupKPIInput> input);
        ServiceResponse Update(SupKPIInput input);
        ServiceResponse Delete(int Id);
        ServiceResponse GetOne(int Id);
        ServiceResponse GetAll();
    }
}
