using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityProfUnit.Controllers
{
    public class ProfileApiBaseController : Controller
    {
        public readonly IMediator _mediator;

        public ProfileApiBaseController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        protected BadRequestObjectResult CreateProblemDetails(string error)
        {
            ModelState.AddModelError("", error);

            var problemDetails = new ValidationProblemDetails(ModelState)
            {
                Status = StatusCodes.Status400BadRequest,
            };

            var result = new BadRequestObjectResult(problemDetails);
            result.ContentTypes.Add("application/problem+json");
            result.ContentTypes.Add("application/problem+xml");
            return result;
        }
    }
}
