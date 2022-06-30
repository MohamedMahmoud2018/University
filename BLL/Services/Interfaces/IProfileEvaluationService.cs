using CORE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IProfileEvaluationService
    {
        ServiceResponse Add(ProfileEvaluationInput input);
        ServiceResponse AddCollection(ICollection<ProfileEvaluationInput> input);
        ServiceResponse Update(ICollection<ProfileEvaluationInput> input);
        ServiceResponse Delete(int Id);
        //ProfileEvaluationOutput GetOne(int Id);
        ServiceResponse GetAll();
        ServiceResponse GetOne(int profileId,int KPICategoryId);
    }

}
