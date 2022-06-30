using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniversityProfUnit.Logic.InterFaces;

namespace UniversityProfUnit.Application.Profiles.Commands.UpdateProfileAcademicDegree
{
    public class UpdateProfileAcademicDegreeCommand : IRequest<Result<int>>
    {
        public int ProfileId { get; set; }
        public List<UpdateProfileAcademicDegreeDto> AcademicDegreesList { get; set; }
    }

    public class UpdateProfileAcademicDegreeCommandHandler : IRequestHandler<UpdateProfileAcademicDegreeCommand, Result<int>>
    {
        private readonly IUniversityProfUnitContext _context;
        public UpdateProfileAcademicDegreeCommandHandler(IUniversityProfUnitContext universityProfUnitContext)
        {
            _context = universityProfUnitContext;
        }
        public async Task<Result<int>> Handle(UpdateProfileAcademicDegreeCommand request, CancellationToken cancellationToken)
        {
            Maybe<Logic.ProfileAgreget.Profile> maybeProfile = await _context.Profiles
                .Include(x => x.ProfileAcademicDegreeList)
                .FirstOrDefaultAsync(x => x.ProfileId == request.ProfileId);

            if (maybeProfile.HasNoValue)
                return Result.Failure<int>(Messages.ProfileNotFound);

            Logic.ProfileAgreget.Profile profile = maybeProfile.Value;

            List<int> academicDegreeIdList = request.AcademicDegreesList.Select(x => x.AcademicDegreeId).ToList();

            List<Logic.AcademicDegree> academicDegreeList = 
                await _context.AcademicDegrees.Where(x => academicDegreeIdList.Contains(x.AcademicDegreeId)).ToListAsync();

            List<Logic.ProfileAgreget.ProfileDtos.ProfileAcademicDegreeDto> academicDegreeDtos = new List<Logic.ProfileAgreget.ProfileDtos.ProfileAcademicDegreeDto>();

            foreach (var item in request.AcademicDegreesList)
            {
                academicDegreeDtos.Add(new Logic.ProfileAgreget.ProfileDtos.ProfileAcademicDegreeDto
                {
                    AcademicDegree = academicDegreeList.FirstOrDefault(x => x.AcademicDegreeId == item.AcademicDegreeId),
                    DegreeDate = item.DegreeDate,
                    Specialty = item.Specialty,
                    University = item.University,
                    ProfileAcademicDegreeId = item.ProfileAcademicDegreeId
                });
            }

            var updateResult = profile.UpdateProfileAcademicDegree(academicDegreeDtos);

            if (updateResult.IsFailure)
                return Result.Failure<int>(updateResult.Error);

            var saveResult = await _context.SaveChangesWithValidation();

            if (saveResult.IsFailure)
                return Result.Failure<int>(saveResult.Error);

            return updateResult.Value.ProfileId;
        }
    }
}
