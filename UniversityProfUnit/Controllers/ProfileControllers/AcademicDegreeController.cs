using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProfUnit.Application.AcademicDegree.Commands.CreateAcademicDegree;
using UniversityProfUnit.Application.AcademicDegree.Dtos;
using UniversityProfUnit.Application.AcademicDegree.Queries.GetAcademicDegree;
using UniversityProfUnit.Application.AcademicDegree.Commands.UpdateAcademicDegree;
using UniversityProfUnit.Application.AcademicDegree.Commands.DeleteAcademicDegree;

namespace UniversityProfUnit.Controllers.ProfileControllers
{
    public class AcademicDegreeController : ProfileApiBaseController
    {
        public AcademicDegreeController(IMediator mediator) : base(mediator)
        {

        }


        [HttpGet]
        [Route("api/AcademicDegree/GetAll")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<AcademicDegreeDto>>> GetAcademicDegreeList()
        {
            var getResult = await _mediator.Send(new GetAcademicDegreeQuery());

            return Ok(getResult);
        }


        [HttpPost]
        [Route("api/AcademicDegree")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<AcademicDegreeDto>>> PostAcademicDegree([FromBody] string name)
        {
            var createResult = await _mediator.Send(new CreateAcademicDegreeCommand { AcademicDegreeName = name });

            if (createResult.IsFailure)
                return CreateProblemDetails(createResult.Error);

            var getResult = await _mediator.Send(new GetAcademicDegreeQuery());

            return Ok(getResult);
        }


        [HttpPut]
        [Route("api/AcademicDegree")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<AcademicDegreeDto>>> PutAcademicDegree([FromBody] UpdateAcademicDegreeCommand updateAcademicDegree)
        {
            var updateResult = await _mediator.Send(updateAcademicDegree);

            if (updateResult.IsFailure)
                return CreateProblemDetails(updateResult.Error);

            var getResult = await _mediator.Send(new GetAcademicDegreeQuery());

            return Ok(getResult);
        }


        [HttpDelete]
        [Route("api/AcademicDegree/{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<AcademicDegreeDto>>> DeleteAcademicDegree(int id)
        {
            var deleteResult = await _mediator.Send(new DeleteAcademicDegreeCommand { AcademicDegreeId = id });

            if (deleteResult.IsFailure)
                return CreateProblemDetails(deleteResult.Error);

            var getResult = await _mediator.Send(new GetAcademicDegreeQuery());

            return Ok(getResult);
        }
    }
}
