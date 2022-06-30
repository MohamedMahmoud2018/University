using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniversityProfUnit.Logic.InterFaces;

namespace UniversityProfUnit.Application.Profiles.Queries.GetProfileById
{
    public class GetProfileByIdQuery : IRequest<Result<GetProfileByProfileIdDto>>
    {
        public int ProfileId { get; set; }
    }

    public class GetProfileByIdQueryHandler : IRequestHandler<GetProfileByIdQuery, Result<GetProfileByProfileIdDto>>
    {
        private readonly IUniversityProfUnitContext _context;
        private readonly IMapper _mapper;

        public GetProfileByIdQueryHandler(IUniversityProfUnitContext universityProfUnitContext,IMapper mapper)
        {
            _context = universityProfUnitContext;
            _mapper = mapper;
        }
        public async Task<Result<GetProfileByProfileIdDto>> Handle(GetProfileByIdQuery request, CancellationToken cancellationToken)
        {
            Maybe<Logic.ProfileAgreget.Profile> profileResult = await _context.Profiles
                .Include(x => x.ProfileAcademicDegreeList)
                .Include(x => x.ProfileCourseList)
                .Include(x => x.ProfileExperienceList)
                .FirstOrDefaultAsync(x => x.ProfileId == request.ProfileId);

            if (profileResult.HasNoValue)
                return Result.Failure<GetProfileByProfileIdDto>(Messages.ProfileNotFound);

            Logic.ProfileAgreget.Profile profile = profileResult.Value;

            GetProfileByProfileIdDto getProfileByProfileIdDto = _mapper.Map<Logic.ProfileAgreget.Profile, GetProfileByProfileIdDto>(profile);

            return getProfileByProfileIdDto;
        }
    }
}
