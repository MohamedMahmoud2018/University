using CORE.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IProfileServices
    {
        ServiceResponse Add(ProfileInput input);
        ServiceResponse Update(ProfileInput input);
        ServiceResponse Delete(int Id);
        ServiceResponse GetOne(int Id);
        //List<ProfileOutput> GetAll();
        ServiceResponse GetAll(int page,string SearchKey=null);
        // ServiceResponse Upload(IFormFile file, string fileType);
        ServiceResponse FilterKeys();
        ServiceResponse FilterData(int page,string SearchKey, string SearchValue);
        ServiceResponse GetEvaluation(int ProfileId);
    }
}
