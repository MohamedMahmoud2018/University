using BLL;
using BLL.Services;
using CORE.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private IGenderService genderService;

        public GenderController(IGenderService _genderService)
        {
            genderService = _genderService;
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add(GenderInput input)
        {
            ServiceResponse success = genderService.Add(input);
            switch (success.Code)
            {
                case 200:
                    return Ok(success);
                case 400:
                    return StatusCode(StatusCodes.Status400BadRequest, success);
                case 404:
                    return StatusCode(StatusCodes.Status404NotFound, success);
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError, success);
            }
        }
       
        [HttpPost]
        [Route("Update")]
        public IActionResult Update(GenderInput input)
        {
            ServiceResponse success = genderService.Update(input);
            switch (success.Code)
            {
                case 200:
                    return Ok(success);
                case 400:
                    return StatusCode(StatusCodes.Status400BadRequest, success);
                case 404:
                    return StatusCode(StatusCodes.Status404NotFound, success);
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError, success);
            }
        }
       
        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(int Id)
        {
            ServiceResponse success = genderService.Delete(Id);
            switch (success.Code)
            {
                case 200:
                    return Ok(success);
                case 400:
                    return StatusCode(StatusCodes.Status400BadRequest, success);
                case 404:
                    return StatusCode(StatusCodes.Status404NotFound, success);
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError, success);
            }
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            ServiceResponse success = genderService.GetAll();

            switch (success.Code)
            {
                case 200:
                    return Ok(success);
                case 400:
                    return StatusCode(StatusCodes.Status400BadRequest, success);
                case 404:
                    return StatusCode(StatusCodes.Status404NotFound, success);
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError, success);
            }
        }
        [HttpGet]
        [Route("GetOne/{Id}")]
        public IActionResult GetOne(int Id)
        {
            ServiceResponse success = genderService.GetOne(Id);

            switch (success.Code)
            {
                case 200:
                    return Ok(success);
                case 400:
                    return StatusCode(StatusCodes.Status400BadRequest, success);
                case 404:
                    return StatusCode(StatusCodes.Status404NotFound, success);
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError, success);
            }
        }
    }
}
