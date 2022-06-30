using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProfUnit.Application.Genders.Commands.CreateGender;
using UniversityProfUnit.Application.Genders.Commands.DeleteGender;
using UniversityProfUnit.Application.Genders.Commands.UpdateGenders;
using UniversityProfUnit.Application.Genders.Dtos;
using UniversityProfUnit.Application.Genders.Queries.GetGender;

namespace UniversityProfUnit.Controllers.ProfileControllers
{
    public class GenderController : ProfileApiBaseController
    {
        public GenderController(IMediator mediator) : base(mediator)
        {

        }


        [HttpGet]
        [Route("api/Gender/GetAll")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<GenderDto>>> GetGenderList()
        {
            var getResult = await _mediator.Send(new GetGenderQuery());

            return Ok(getResult);
        }


        [HttpPost]
        [Route("api/Gender")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<GenderDto>>> PostGender([FromBody] string name)
        {
            var createResult = await _mediator.Send(new CreateGenderCommand { GenderName = name });

            if (createResult.IsFailure)
                return CreateProblemDetails(createResult.Error);

            var getResult = await _mediator.Send(new GetGenderQuery());

            return Ok(getResult);
        }


        [HttpPut]
        [Route("api/Gender")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<GenderDto>>> PutGender([FromBody] UpdateGenderCommand updateGender)
        {
            var updateResult = await _mediator.Send(updateGender);

            if (updateResult.IsFailure)
                return CreateProblemDetails(updateResult.Error);

            var getResult = await _mediator.Send(new GetGenderQuery());

            return Ok(getResult);
        }


        [HttpDelete]
        [Route("api/Gender/{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<GenderDto>>> DeleteGender(int id)
        {
            var deleteResult = await _mediator.Send(new DeleteGenderCommand { GenderId = id });

            if (deleteResult.IsFailure)
                return CreateProblemDetails(deleteResult.Error);

            var getResult = await _mediator.Send(new GetGenderQuery());

            return Ok(getResult);
        }
    }
}
