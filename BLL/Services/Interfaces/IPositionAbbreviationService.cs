using CORE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
   public interface IPositionAbbreviationService
    {
        ServiceResponse Add(PositionAbbreviationInput input);
        ServiceResponse Update(PositionAbbreviationInput input);
        ServiceResponse Delete(int Id);
        ServiceResponse GetOne(int Id);
        ServiceResponse GetAll();
    }
}
