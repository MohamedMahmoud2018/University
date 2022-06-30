using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProfUnit.Application.Specialties.Commands.CreateSpecialty;
using UniversityProfUnit.Application.Specialties.SpecialtyDtos;
using UniversityProfUnit.Application.Specialties.Queries.GetSpecialty;
using UniversityProfUnit.Application.Specialties.Commands.UpdateSpecialty;
using UniversityProfUnit.Application.Specialties.Commands.Deletespecialty;

namespace UniversityProfUnit.Controllers.ProfileControllers
{
    public class SpecialtyController : ProfileApiBaseController
    {
        public SpecialtyController(IMediator mediator) : base(mediator)
        {

        }


        [HttpGet]
        [Route("api/Specialty/GetAll")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<SpecialtyDto>>> GetSpecialtyList()
        {
            var getResult = await _mediator.Send(new GetSpecialtyQuery());

            return Ok(getResult);
        }


        [HttpPost]
        [Route("api/Specialty")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<SpecialtyDto>>> PostSpecialty([FromBody] string name)
        {
            var createResult = await _mediator.Send(new CreateSpecialtyCommand { SpecialtyName = name });

            if (createResult.IsFailure)
                return CreateProblemDetails(createResult.Error);

            var getResult = await _mediator.Send(new GetSpecialtyQuery());

            return Ok(getResult);
        }


        [HttpPut]
        [Route("api/Specialty")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<SpecialtyDto>>> PutSpecialty([FromBody] UpdateSpecialtyCommand updateSpecialty)
        {
            var updateResult = await _mediator.Send(updateSpecialty);

            if (updateResult.IsFailure)
                return CreateProblemDetails(updateResult.Error);

            var getResult = await _mediator.Send(new GetSpecialtyQuery());

            return Ok(getResult);
        }


        [HttpDelete]
        [Route("api/Specialty/{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<SpecialtyDto>>> DeleteSpecialty(int id)
        {
            var deleteResult = await _mediator.Send(new DeletespecialtyCommand { SpecialtyId = id });

            if (deleteResult.IsFailure)
                return CreateProblemDetails(deleteResult.Error);

            var getResult = await _mediator.Send(new GetSpecialtyQuery());

            return Ok(getResult);
        }
    }
}
