using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProfUnit.Application.Profiles.Commands.CreateProfile;
using UniversityProfUnit.Application.Profiles.Commands.DeleteProfile;
using UniversityProfUnit.Application.Profiles.Commands.UpdateAdditionalInfo;
using UniversityProfUnit.Application.Profiles.Commands.UpdateProfileAcademicDegree;
using UniversityProfUnit.Application.Profiles.Commands.UpdateProfileCourses;
using UniversityProfUnit.Application.Profiles.Commands.UpdateProfileExperiences;
using UniversityProfUnit.Application.Profiles.Commands.UpdateProfileMain;
using UniversityProfUnit.Application.Profiles.Queries.GetProfileById;
using UniversityProfUnit.Application.Profiles.Queries.GetProfileList;

namespace UniversityProfUnit.Controllers.ProfileControllers
{
    public class ProfileController : ProfileApiBaseController
    {
        public ProfileController(IMediator mediator) : base(mediator)
        {

        }


        [HttpGet]
        [Route("api/Profile/GetAll")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<GetProfileListDto>>> GetProfileList()
        {
            var getResult = await _mediator.Send(new GetProfileListQuery());

            return Ok(getResult);
        }


        [HttpGet]
        [Route("api/Profile/{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetProfileByProfileIdDto>> GetProfileById(int id)
        {
            var getResult = await _mediator.Send(new GetProfileByIdQuery() { ProfileId = id});

            if (getResult.IsFailure)
                return CreateProblemDetails(getResult.Error);

            return Ok(getResult.Value);
        }


        [HttpPost]
        [Route("api/Profile")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetProfileByProfileIdDto>> PostProfile([FromBody] CreateProfileCommand createCommand)
        {
            var createResult = await _mediator.Send(createCommand);

            if (createResult.IsFailure)
                return CreateProblemDetails(createResult.Error);

            var getResult = await _mediator.Send(new GetProfileByIdQuery() { ProfileId = createResult.Value });

            if (getResult.IsFailure)
                return CreateProblemDetails(getResult.Error);

            return Ok(getResult.Value);
        }


        [HttpPut]
        [Route("api/Profile/Main")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetProfileByProfileIdDto>> PutProfileMain([FromBody] UpdateProfileMainCommand updateProfile)
        {
            var updateResult = await _mediator.Send(updateProfile);

            if (updateResult.IsFailure)
                return CreateProblemDetails(updateResult.Error);

            var getResult = await _mediator.Send(new GetProfileByIdQuery() { ProfileId = updateResult.Value });

            if (getResult.IsFailure)
                return CreateProblemDetails(getResult.Error);

            return Ok(getResult.Value);
        }


        [HttpPut]
        [Route("api/Profile/AcademicDegree")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetProfileByProfileIdDto>> PutProfileAcademicDegree([FromBody] UpdateProfileAcademicDegreeCommand updateProfile)
        {
            var updateResult = await _mediator.Send(updateProfile);

            if (updateResult.IsFailure)
                return CreateProblemDetails(updateResult.Error);

            var getResult = await _mediator.Send(new GetProfileByIdQuery() { ProfileId = updateResult.Value });

            if (getResult.IsFailure)
                return CreateProblemDetails(getResult.Error);

            return Ok(getResult.Value);
        }


        [HttpPut]
        [Route("api/Profile/Courses")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetProfileByProfileIdDto>> PutProfileCourses([FromBody] UpdateProfileCoursesCommand updateProfile)
        {
            var updateResult = await _mediator.Send(updateProfile);

            if (updateResult.IsFailure)
                return CreateProblemDetails(updateResult.Error);

            var getResult = await _mediator.Send(new GetProfileByIdQuery() { ProfileId = updateResult.Value });

            if (getResult.IsFailure)
                return CreateProblemDetails(getResult.Error);

            return Ok(getResult.Value);
        }


        [HttpPut]
        [Route("api/Profile/Experiences")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetProfileByProfileIdDto>> PutProfileExperiences([FromBody] UpdateProfileExperiencesCommand updateProfile)
        {
            var updateResult = await _mediator.Send(updateProfile);

            if (updateResult.IsFailure)
                return CreateProblemDetails(updateResult.Error);

            var getResult = await _mediator.Send(new GetProfileByIdQuery() { ProfileId = updateResult.Value });

            if (getResult.IsFailure)
                return CreateProblemDetails(getResult.Error);

            return Ok(getResult.Value);
        }


        [HttpPut]
        [Route("api/Profile/AdditionalInfo")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetProfileByProfileIdDto>> PutProfileAdditionalInfo([FromBody] UpdateAdditionalInfoCommand updateProfile)
        {
            var updateResult = await _mediator.Send(updateProfile);

            if (updateResult.IsFailure)
                return CreateProblemDetails(updateResult.Error);

            var getResult = await _mediator.Send(new GetProfileByIdQuery() { ProfileId = updateResult.Value });

            if (getResult.IsFailure)
                return CreateProblemDetails(getResult.Error);

            return Ok(getResult.Value);
        }


        [HttpDelete]
        [Route("api/Profile/{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            var deleteResult = await _mediator.Send(new DeleteProfileCommand { ProfileId = id });

            if (deleteResult.IsFailure)
                return CreateProblemDetails(deleteResult.Error);

            return Ok();
        }
    }
}
