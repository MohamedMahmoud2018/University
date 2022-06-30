using BLL;
using BLL.Services;
using CORE.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProfileController : ControllerBase
    {
        private IProfileServices ProfileServices;

        public ProfileController(IProfileServices _ProfileServices)
        {
            ProfileServices = _ProfileServices;
        }
        
        // [HttpPost]
        [HttpPost, DisableRequestSizeLimit]
        [Consumes("multipart/form-data")]
        [Route("Add")]
        public IActionResult Add([FromForm]ProfileInput input)
        {
            //var file = Request.Form.Files[0];
            //if (!ModelState.IsValid)
            //    return new IActionResult
            //    {
            //        IsError = true,
            //        //Data = (from item in ModelState
            //        //        where item.Value.Errors.Any()
            //        //        select item.Value.Errors[0].ErrorMessage).ToList(),
            //        Message="الرجاء التأكد من جميع البيانات"
            //    };
            ServiceResponse success = ProfileServices.Add(input);
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
       
        [HttpPost, DisableRequestSizeLimit]
        [Consumes("multipart/form-data")]
        [Route("Update")]
        public IActionResult Update([FromForm] ProfileInput input)
        {
            
            ServiceResponse success = ProfileServices.Update(input);
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
            ServiceResponse success = ProfileServices.Delete(Id);
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
        [Route("GetAll/{page}")]
        public IActionResult GetAll( int page, [FromQuery] string SearchKey = null)
        {
            ServiceResponse success = ProfileServices.GetAll(page, SearchKey);

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
            ServiceResponse success = ProfileServices.GetOne(Id);

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
        [Route("GetEvaluation/{Id}")]
        public IActionResult GetEvaluation(int Id)
        {
            ServiceResponse success = ProfileServices.GetEvaluation(Id);

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
        [Route("GetFilterKeys")]
        public IActionResult GetFilterKeys()
        {
            ServiceResponse success = ProfileServices.FilterKeys();

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
        [Route("Filter/{page}")]
        public IActionResult Filter(int Page,[FromQuery] string SearchKey, [FromQuery] string SearchValue)
        {
            ServiceResponse success = ProfileServices.FilterData(Page,SearchKey, SearchValue);

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
