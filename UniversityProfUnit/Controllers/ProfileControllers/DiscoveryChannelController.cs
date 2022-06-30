using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProfUnit.Application.DiscoveryChannel.Commands.CreateDiscoveryChannel;
using UniversityProfUnit.Application.DiscoveryChannel.Dtos;
using UniversityProfUnit.Application.DiscoveryChannel.Queries.GetDiscoveryChannel;
using UniversityProfUnit.Application.DiscoveryChannel.Commands.UpdateDiscoveryChannel;
using UniversityProfUnit.Application.DiscoveryChannel.Commands.DeleteDiscoveryChannel;

namespace UniversityProfUnit.Controllers.ProfileControllers
{
    public class DiscoveryChannelController : ProfileApiBaseController
    {
        public DiscoveryChannelController(IMediator mediator) : base(mediator)
        {

        }


        [HttpGet]
        [Route("api/DiscoveryChannel/GetAll")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<DiscoveryChannelDto>>> GetDiscoveryChannelList()
        {
            var getResult = await _mediator.Send(new GetDiscoveryChannelQuery());

            return Ok(getResult);
        }


        [HttpPost]
        [Route("api/DiscoveryChannel")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<DiscoveryChannelDto>>> PostDiscoveryChannel([FromBody] string name)
        {
            var createResult = await _mediator.Send(new CreateDiscoveryChannelCommand { DiscoveryChannelName = name });

            if (createResult.IsFailure)
                return CreateProblemDetails(createResult.Error);

            var getResult = await _mediator.Send(new GetDiscoveryChannelQuery());

            return Ok(getResult);
        }


        [HttpPut]
        [Route("api/DiscoveryChannel")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<DiscoveryChannelDto>>> PutDiscoveryChannel([FromBody] UpdateDiscoveryChannelCommand updateDiscoveryChannel)
        {
            var updateResult = await _mediator.Send(updateDiscoveryChannel);

            if (updateResult.IsFailure)
                return CreateProblemDetails(updateResult.Error);

            var getResult = await _mediator.Send(new GetDiscoveryChannelQuery());

            return Ok(getResult);
        }


        [HttpDelete]
        [Route("api/DiscoveryChannel/{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<DiscoveryChannelDto>>> DeleteDiscoveryChannel(int id)
        {
            var deleteResult = await _mediator.Send(new DeleteDiscoveryChannelCommand { DiscoveryChannelId = id });

            if (deleteResult.IsFailure)
                return CreateProblemDetails(deleteResult.Error);

            var getResult = await _mediator.Send(new GetDiscoveryChannelQuery());

            return Ok(getResult);
        }
    }
}
