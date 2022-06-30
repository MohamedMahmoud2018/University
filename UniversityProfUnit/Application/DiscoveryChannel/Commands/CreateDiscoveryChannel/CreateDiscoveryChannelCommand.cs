using CSharpFunctionalExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniversityProfUnit.Logic.InterFaces;

namespace UniversityProfUnit.Application.DiscoveryChannel.Commands.CreateDiscoveryChannel
{
    public class CreateDiscoveryChannelCommand : IRequest<Result<int>>
    {
        public string DiscoveryChannelName { get; set; }
    }

    public class CreateDiscoveryChannelCommandHandler : IRequestHandler<CreateDiscoveryChannelCommand, Result<int>>
    {
        private readonly IUniversityProfUnitContext _context;
        public CreateDiscoveryChannelCommandHandler(IUniversityProfUnitContext universityProfUnitContext)
        {
            _context = universityProfUnitContext;
        }
        public async Task<Result<int>> Handle(CreateDiscoveryChannelCommand request, CancellationToken cancellationToken)
        {
            var createResult = Logic.DiscoveryChannel.CreateDiscoveryChannel(request.DiscoveryChannelName);

            if (createResult.IsFailure)
                return Result.Failure<int>(createResult.Error);

            _context.DiscoveryChannels.Add(createResult.Value);

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success(createResult.Value.DiscoveryChannelId);
        }
    }
}
