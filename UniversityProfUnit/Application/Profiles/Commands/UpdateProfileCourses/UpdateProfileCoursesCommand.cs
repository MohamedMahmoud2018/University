using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniversityProfUnit.Logic.InterFaces;

namespace UniversityProfUnit.Application.Profiles.Commands.UpdateProfileCourses
{
    public class UpdateProfileCoursesCommand : IRequest<Result<int>>
    {
        public int ProfileId { get; set; }
        public List<UpdateProfileCoursesDto> CourseList { get; set; }
    }

    public class UpdateProfileCoursesCommandHandler : IRequestHandler<UpdateProfileCoursesCommand, Result<int>>
    {
        private readonly IUniversityProfUnitContext _context;
        public UpdateProfileCoursesCommandHandler(IUniversityProfUnitContext universityProfUnitContext)
        {
            _context = universityProfUnitContext;
        }
        public async Task<Result<int>> Handle(UpdateProfileCoursesCommand request, CancellationToken cancellationToken)
        {
            Maybe<Logic.ProfileAgreget.Profile> maybeProfile = await _context.Profiles
               .Include(x => x.ProfileCourseList)
               .FirstOrDefaultAsync(x => x.ProfileId == request.ProfileId);

            if (maybeProfile.HasNoValue)
                return Result.Failure<int>(Messages.ProfileNotFound);

            Logic.ProfileAgreget.Profile profile = maybeProfile.Value;

            List<Logic.ProfileAgreget.ProfileDtos.ProfileCoursesDto> coursesDtos = new List<Logic.ProfileAgreget.ProfileDtos.ProfileCoursesDto>();

            foreach (var item in request.CourseList)
            {
                coursesDtos.Add(new Logic.ProfileAgreget.ProfileDtos.ProfileCoursesDto
                {
                    ProfileCourseId = item.ProfileCourseId,
                    CourseName = item.CourseName,
                    Description = item.Description,
                    FromDate = item.FromDate,
                    ToDate = item.ToDate
                });
            }

            var updateResult = profile.UpdateProfileCourses(coursesDtos);

            if (updateResult.IsFailure)
                return Result.Failure<int>(updateResult.Error);

            var saveResult = await _context.SaveChangesWithValidation();

            if (saveResult.IsFailure)
                return Result.Failure<int>(saveResult.Error);

            return updateResult.Value.ProfileId;
        }
    }
}
