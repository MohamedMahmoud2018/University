using CORE.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IAuthenticateService
    {
        public Task<ServiceResponse> Register(RegisterModel model);
        public  Task<ServiceResponse> Login(LoginModel model);
    }
}
