using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniversityProfUnit.Logic.InterFaces;

namespace UniversityProfUnit.Application.Profiles.Commands.DeleteProfile
{
    public class DeleteProfileCommand : IRequest<Result>
    {
        public int ProfileId { get; set; }
    }

    public class DeleteProfileCommandHandler : IRequestHandler<DeleteProfileCommand, Result>
    {
        private readonly IUniversityProfUnitContext _context;
        public DeleteProfileCommandHandler(IUniversityProfUnitContext universityProfUnitContext)
        {
            _context = universityProfUnitContext;
        }
        public async Task<Result> Handle(DeleteProfileCommand request, CancellationToken cancellationToken)
        {
            Maybe<Logic.ProfileAgreget.Profile> maybeProfile = await _context.Profiles
                .Include(x => x.ProfileAcademicDegreeList)
                .Include(x => x.ProfileCourseList)
                .Include(x => x.ProfileExperienceList)
                .FirstOrDefaultAsync(x => x.ProfileId == request.ProfileId);

            if (maybeProfile.HasNoValue)
                return Result.Failure(Messages.ProfileNotFound);

            Logic.ProfileAgreget.Profile profile = maybeProfile.Value;

            //Validate Delete

            var deleteResult = _context.Profiles.Remove(profile);

            var saveResult = await _context.SaveChangesWithValidation();

            if (saveResult.IsFailure)
                return Result.Failure<int>(saveResult.Error);

            return Result.Success();
        }
    }
}
