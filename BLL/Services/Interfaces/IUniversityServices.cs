using CORE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
   public interface IUniversityServices
    {
        ServiceResponse Add(UniversityInput input);
        ServiceResponse Update(UniversityInput input);
        ServiceResponse Delete(int Id);
        ServiceResponse GetOne(int Id);
        ServiceResponse GetAll();
        ServiceResponse GetByCityID(int cityId);
    }
}
