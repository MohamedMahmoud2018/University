using CORE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
   public interface IAcademicRankService
    {
        ServiceResponse Add(AcademicRankInput input);
        ServiceResponse Update(AcademicRankInput input);
        ServiceResponse Delete(int Id);
        ServiceResponse GetOne(int Id);
        ServiceResponse GetAll();
    }
}
