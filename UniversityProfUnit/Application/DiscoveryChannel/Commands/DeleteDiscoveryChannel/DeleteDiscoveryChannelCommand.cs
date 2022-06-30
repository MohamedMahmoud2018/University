using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniversityProfUnit.Logic.InterFaces;

namespace UniversityProfUnit.Application.DiscoveryChannel.Commands.DeleteDiscoveryChannel
{
    public class DeleteDiscoveryChannelCommand : IRequest<Result>
    {
        public int DiscoveryChannelId { get; set; }
    }

    public class DeleteDiscoveryChannelCommandHandler : IRequestHandler<DeleteDiscoveryChannelCommand, Result>
    {
        private readonly IUniversityProfUnitContext _context;
        public DeleteDiscoveryChannelCommandHandler(IUniversityProfUnitContext universityProfUnitContext)
        {
            _context = universityProfUnitContext;
        }

        public async Task<Result> Handle(DeleteDiscoveryChannelCommand request, CancellationToken cancellationToken)
        {
            Maybe<Logic.DiscoveryChannel> DiscoveryChannelResult = 
                await _context.DiscoveryChannels.FirstOrDefaultAsync(x => x.DiscoveryChannelId == request.DiscoveryChannelId);

            if (DiscoveryChannelResult.HasNoValue)
                return Result.Failure(Messages.DiscoveryChannelNotFound);

            Logic.DiscoveryChannel discoveryChannel = DiscoveryChannelResult.Value;

            List<int> usedIds = await _context.Profiles.Select(x => x.DiscoveryChannelId ?? 0).ToListAsync();

            var validateDeleteResult = discoveryChannel.ValidateForDelete(usedIds);

            if (validateDeleteResult.IsFailure)
                return Result.Failure(validateDeleteResult.Error);

            var deleteResult = _context.DiscoveryChannels.Remove(discoveryChannel);

            var saveResult = await _context.SaveChangesWithValidation();

            if (saveResult.IsFailure)
                return Result.Failure<int>(saveResult.Error);

            return Result.Success();
        }
    }
}
