using BLL;
using BLL.Services;
using CORE.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicRankController : ControllerBase
    {
        private IAcademicRankService AcademicRankService;

        public AcademicRankController(IAcademicRankService _AcademicRankService)
        {
            AcademicRankService = _AcademicRankService;
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add(AcademicRankInput input)
        {
            ServiceResponse success = AcademicRankService.Add(input);
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
        public IActionResult Update(AcademicRankInput input)
        {
            ServiceResponse success = AcademicRankService.Update(input);
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
            ServiceResponse success = AcademicRankService.Delete(Id);
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
            ServiceResponse success = AcademicRankService.GetAll();

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
            ServiceResponse success = AcademicRankService.GetOne(Id);

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
