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
    public class ProfileScientificResearchController : ControllerBase
    {
        IProfileScientificResearchService ProfileScientificResearchService;

        public ProfileScientificResearchController(IProfileScientificResearchService _ProfileScientificResearchService)
        {
            ProfileScientificResearchService = _ProfileScientificResearchService;
        }
        [HttpPost]
        [Route("Add")]
        public IActionResult Add(ProfileScientificResearchInput input)
        {
            ServiceResponse success = ProfileScientificResearchService.Add(input);
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
        public IActionResult Update(ProfileScientificResearchInput input)
        {
            ServiceResponse success = ProfileScientificResearchService.Update(input);
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
            ServiceResponse success = ProfileScientificResearchService.Delete(Id);
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
            ServiceResponse success = ProfileScientificResearchService.GetAll();

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

        //[HttpGet]
        //[Route("GetAllByProfileId/{profileId}")]
        //public IActionResult GetAllByProfileId(int profileId)
        //{

        //    ServiceResponse success = ProfileScientificResearchService.GetAllByProfileId(profileId);

        //    switch (success.Code)
        //    {
        //        case 200:
        //            return Ok(success);
        //        case 400:
        //            return StatusCode(StatusCodes.Status400BadRequest, success);
        //        case 404:
        //            return StatusCode(StatusCodes.Status404NotFound, success);
        //        default:
        //            return StatusCode(StatusCodes.Status500InternalServerError, success);
        //    }
        //}


        [HttpGet]
        [Route("GetOne/{Id}")]
        public IActionResult GetOne(int Id)
        {
            ServiceResponse success = ProfileScientificResearchService.GetOne(Id);

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
