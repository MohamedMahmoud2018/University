using CORE.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IProfileAcademicDegreeService
    {
        ServiceResponse Add(ProfileAcademicDegreeInput input);
        ServiceResponse Update(ProfileAcademicDegreeInput input);
        ServiceResponse Delete(int Id);
        ServiceResponse GetOne(int Id);
        ServiceResponse GetAll();

        ServiceResponse GetAllByProfileId(int profileId);
    }
}
