using CORE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
   public interface ICityService
    {
        ServiceResponse Add(CityInput input);
        ServiceResponse Update(CityInput input);
        ServiceResponse Delete(int Id);
        ServiceResponse GetOne(int Id);
        ServiceResponse GetAll();
        ServiceResponse GetByCountryID(int countryId);

    }
}
