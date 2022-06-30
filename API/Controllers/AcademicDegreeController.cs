using BLL;
using BLL.Services;
using CORE.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
   
    public class AcademicDegreeController : ControllerBase
    {
        private IAcademicDegreeService academicDegreeService;

        public AcademicDegreeController(IAcademicDegreeService _AcademicDegreeService)
        {
            academicDegreeService = _AcademicDegreeService;
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add(AcademicDegreeInput input)
        {
            ServiceResponse success = academicDegreeService.Add(input);
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
        public IActionResult Update(AcademicDegreeInput input)
        {
            ServiceResponse success = academicDegreeService.Update(input);
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
            ServiceResponse success = academicDegreeService.Delete(Id);
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
        //[Authorize]
        [HttpGet]
        [Route("GetAll/{IsLookup?}")]
        public IActionResult GetAll(bool IsLookup = false)
        {
            ServiceResponse success = academicDegreeService.GetAll(IsLookup);

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
            ServiceResponse success = academicDegreeService.GetOne(Id);

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