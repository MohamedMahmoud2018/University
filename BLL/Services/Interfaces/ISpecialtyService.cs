using CORE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface ISpecialtyService
    {
        ServiceResponse Add(SpecialtyInput input);
        ServiceResponse Update(SpecialtyInput input);
        ServiceResponse Delete(int Id);
        ServiceResponse GetOne(int Id);
        ServiceResponse GetAll(bool IsLookup);
    }
}
