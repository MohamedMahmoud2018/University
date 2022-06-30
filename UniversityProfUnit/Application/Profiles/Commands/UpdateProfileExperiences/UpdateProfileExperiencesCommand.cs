using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniversityProfUnit.Logic.InterFaces;

namespace UniversityProfUnit.Application.Profiles.Commands.UpdateProfileExperiences
{
    public class UpdateProfileExperiencesCommand : IRequest<Result<int>>
    {
        public int ProfileId { get; set; }
        public List<UpdateProfileExperiencesDto> experiencesList { get; set; }
    }

    public class UpdateProfileExperiencesCommandHandler : IRequestHandler<UpdateProfileExperiencesCommand, Result<int>>
    {
        private readonly IUniversityProfUnitContext _context;
        public UpdateProfileExperiencesCommandHandler(IUniversityProfUnitContext universityProfUnitContext)
        {
            _context = universityProfUnitContext;
        }
        public async Task<Result<int>> Handle(UpdateProfileExperiencesCommand request, CancellationToken cancellationToken)
        {
            Maybe<Logic.ProfileAgreget.Profile> maybeProfile = await _context.Profiles
                .Include(x => x.ProfileExperienceList)
                .FirstOrDefaultAsync(x => x.ProfileId == request.ProfileId);

            if (maybeProfile.HasNoValue)
                return Result.Failure<int>(Messages.ProfileNotFound);

            Logic.ProfileAgreget.Profile profile = maybeProfile.Value;

            List<Logic.ProfileAgreget.ProfileDtos.ProfileExperiencesDto> experiencesDtos = new List<Logic.ProfileAgreget.ProfileDtos.ProfileExperiencesDto>();

            foreach (var item in request.experiencesList)
            {
                experiencesDtos.Add(new Logic.ProfileAgreget.ProfileDtos.ProfileExperiencesDto
                {
                    ProfileExperienceId = item.ProfileExperienceId,
                    Description = item.Description,
                    Employer = item.Employer,
                    FromDate = item.FromDate,
                    JopName = item.JopName,
                    ToDate = item.ToDate
                });
            }

            var updateResult = profile.UpdateProfileExperiences(experiencesDtos);

            if (updateResult.IsFailure)
                return Result.Failure<int>(updateResult.Error);

            var saveResult = await _context.SaveChangesWithValidation();

            if (saveResult.IsFailure)
                return Result.Failure<int>(saveResult.Error);

            return updateResult.Value.ProfileId;
        }
    }
}
