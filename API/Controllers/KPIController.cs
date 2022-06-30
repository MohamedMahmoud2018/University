using BLL;
using BLL.Services;
using CORE.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class KPIController : ControllerBase
    {
        private IKPIService KPIService;
        public KPIController(IKPIService _KPIService)
        {
            KPIService = _KPIService;
        }

        //[HttpPost]
        //[Route("Add")]
        //public IActionResult Add(KPIInput input)
        //{

        //    ServiceResponse success = KPIService.Add(input);
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

        [HttpPost]
        [Route("AddCollection")]
        public IActionResult AddCollection(ICollection<KPIInput> input)
        {
            ServiceResponse success = KPIService.AddCollection(input);
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

        //[HttpPost]
        //[Route("Update")]
        //public IActionResult Update(ICollection< KPIInput> input)
        //{
        //    ServiceResponse success = KPIService.Update(input);
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

        //[HttpPost]
        //[Route("Delete")]
        //public IActionResult Delete(int Id)
        //{
        //    ServiceResponse success = KPIService.Delete(Id);
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
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            ServiceResponse success = KPIService.GetAll();

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
            ServiceResponse success = KPIService.GetOne(Id);

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
