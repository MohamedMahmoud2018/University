using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniversityProfUnit.Logic.InterFaces;

namespace UniversityProfUnit.Application.DiscoveryChannel.Commands.UpdateDiscoveryChannel
{
    public class UpdateDiscoveryChannelCommand : IRequest<Result<int>>
    {
        public int DiscoveryChannelId { get; set; }
        public string DiscoveryChannelName { get; set; }
    }

    public class UpdateDiscoveryChannelCommandHandler : IRequestHandler<UpdateDiscoveryChannelCommand, Result<int>>
    {

        private readonly IUniversityProfUnitContext _context;
        public UpdateDiscoveryChannelCommandHandler(IUniversityProfUnitContext universityProfUnitContext)
        {
            _context = universityProfUnitContext;
        }

        public async Task<Result<int>> Handle(UpdateDiscoveryChannelCommand request, CancellationToken cancellationToken)
        {
            Maybe<Logic.DiscoveryChannel> DiscoveryChannelResult = 
                await _context.DiscoveryChannels.FirstOrDefaultAsync(x => x.DiscoveryChannelId == request.DiscoveryChannelId);

            if (DiscoveryChannelResult.HasNoValue)
                return Result.Failure<int>(Messages.DiscoveryChannelNotFound);

            Logic.DiscoveryChannel DiscoveryChannel = DiscoveryChannelResult.Value;

            Result<Logic.DiscoveryChannel> updateResult = DiscoveryChannel.UpdateDiscoveryChannel(request.DiscoveryChannelName);

            if (updateResult.IsFailure)
                return Result.Failure<int>(updateResult.Error);

            var saveResult = await _context.SaveChangesWithValidation();

            if (saveResult.IsFailure)
                return Result.Failure<int>(saveResult.Error);

            return Result.Success( updateResult.Value.DiscoveryChannelId);
        }
    }
}
