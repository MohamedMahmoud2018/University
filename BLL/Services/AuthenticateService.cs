using CORE.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
   public class AuthenticateService : IAuthenticateService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticateService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<ServiceResponse> Register(RegisterModel model)
        {
            try
            {
                var userExists = await userManager.FindByEmailAsync(model.Email);
                if (userExists != null)
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "هذا المستخدم موجود بالفعل",
                        Data = StatusCodes.Status500InternalServerError
                    };
                ApplicationUser user = new ApplicationUser()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Username
                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                    return new ServiceResponse
                    {
                        IsError = true,
                        Message = "لم يتم إنشاء مستخدم الرجاء التحقق من البيانات و المحاولة مجدداً",
                        Data = StatusCodes.Status500InternalServerError
                    };
                return new ServiceResponse
                {
                    IsError = false,
                    Message = "تم إنشاء المستخدم الرجاء تسجيل الدخول",
                    Data = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse
                {
                    IsError = true,
                    Message = ex.Message,
                    Data = StatusCodes.Status500InternalServerError
                };
            }
           // throw new NotImplementedException();
        }
        public async Task<ServiceResponse> Login(LoginModel model)
        {
            
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                     new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
                return new ServiceResponse
                {
                    IsError = false,
                    Message = "تم تسجيل الدخول بنجاح",
                    Data = new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    },
                    Code=200
                };
            }
            return new ServiceResponse
            {
                IsError = true,
                Message = "الرجاء التأكد من البيانات",
                Data =null,
                Code=401
                    };
        }

       
    }
}
