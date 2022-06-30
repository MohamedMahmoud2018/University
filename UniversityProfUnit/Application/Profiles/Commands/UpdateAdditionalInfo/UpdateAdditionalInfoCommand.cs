using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniversityProfUnit.Logic.InterFaces;

namespace UniversityProfUnit.Application.Profiles.Commands.UpdateAdditionalInfo
{
    public class UpdateAdditionalInfoCommand : IRequest<Result<int>>
    {
        public int ProfileId { get; set; }
        public int DiscoveryChannelId { get; set; }
        public string Description { get; set; }
    }

    public class UpdateAdditionalInfoCommandHandler : IRequestHandler<UpdateAdditionalInfoCommand, Result<int>>
    {
        private readonly IUniversityProfUnitContext _context;
        public UpdateAdditionalInfoCommandHandler(IUniversityProfUnitContext universityProfUnitContext)
        {
            _context = universityProfUnitContext;
        }
        public async Task<Result<int>> Handle(UpdateAdditionalInfoCommand request, CancellationToken cancellationToken)
        {
            Maybe<Logic.ProfileAgreget.Profile> maybeProfile = await _context.Profiles
                .FirstOrDefaultAsync(x => x.ProfileId == request.ProfileId);

            if (maybeProfile.HasNoValue)
                return Result.Failure<int>(Messages.ProfileNotFound);

            Logic.ProfileAgreget.Profile profile = maybeProfile.Value;

            Maybe<Logic.DiscoveryChannel> maybeDiscoveryChannel = 
                await _context.DiscoveryChannels.FirstOrDefaultAsync(x => x.DiscoveryChannelId == request.DiscoveryChannelId);

            var updateResult = profile.UpdateProfileAdditionalInfo(maybeDiscoveryChannel,request.Description);

            if (updateResult.IsFailure)
                return Result.Failure<int>(updateResult.Error);

            var saveResult = await _context.SaveChangesWithValidation();

            if (saveResult.IsFailure)
                return Result.Failure<int>(saveResult.Error);

            return updateResult.Value.ProfileId;
        }
    }
}
