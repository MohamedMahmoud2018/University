using CORE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IProfileExperiencesService
    {
        ServiceResponse Add(ProfileExperiencesInput input);
        ServiceResponse Update(ProfileExperiencesInput input);
        ServiceResponse Delete(int Id);
        ServiceResponse GetOne(int Id);
        ServiceResponse GetAll();
        ServiceResponse GetAllByProfileId(int profileId);
    }
}
