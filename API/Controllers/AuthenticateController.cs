using BLL.Services;
using CORE.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        //private readonly UserManager<ApplicationUser> userManager;
        //private readonly RoleManager<IdentityRole> roleManager;
        //private readonly IConfiguration _configuration;
        private readonly IAuthenticateService authenticateService;
        public AuthenticateController(IAuthenticateService _authenticateService)//(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            //this.userManager = userManager;
            //this.roleManager = roleManager;
            //_configuration = configuration;
            authenticateService = _authenticateService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var response = await authenticateService.Register(model);
            if (response.IsError)
                return StatusCode(StatusCodes.Status500InternalServerError, response);

            return Ok(response);
        }

            [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var response =await authenticateService.Login(model);
            if(response.IsError)
                return Unauthorized();
            return Ok(response);
        }
        }
}
