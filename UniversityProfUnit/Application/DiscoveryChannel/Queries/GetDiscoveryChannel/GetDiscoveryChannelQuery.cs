using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniversityProfUnit.Application.DiscoveryChannel.Dtos;
using UniversityProfUnit.Logic.InterFaces;

namespace UniversityProfUnit.Application.DiscoveryChannel.Queries.GetDiscoveryChannel
{
    public class GetDiscoveryChannelQuery : IRequest<List<DiscoveryChannelDto>>
    {
    }

    public class GetDiscoveryChannelQueryHandler : IRequestHandler<GetDiscoveryChannelQuery, List<DiscoveryChannelDto>>
    {
        private readonly IUniversityProfUnitContext _context;
        public GetDiscoveryChannelQueryHandler(IUniversityProfUnitContext universityProfUnitContext)
        {
            _context = universityProfUnitContext;
        }
        public async Task<List<DiscoveryChannelDto>> Handle(GetDiscoveryChannelQuery request, CancellationToken cancellationToken)
        {
            List<DiscoveryChannelDto> result = new List<DiscoveryChannelDto>();

            result = await _context.DiscoveryChannels.Select(x => new DiscoveryChannelDto
            {
                DiscoveryChannelId = x.DiscoveryChannelId,
                DiscoveryChannelName = x.DiscoveryChannelName
            }).ToListAsync();

            return result;
        }
    }
}
