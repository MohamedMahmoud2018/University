using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniversityProfUnit.Logic.InterFaces;

namespace UniversityProfUnit.Application.AcademicDegree.Commands.UpdateAcademicDegree
{
    public class UpdateAcademicDegreeCommand : IRequest<Result<int>>
    {
        public int AcademicDegreeId { get; set; }
        public string AcademicDegreeName { get; set; }
    }

    public class UpdateAcademicDegreeCommandHandler : IRequestHandler<UpdateAcademicDegreeCommand, Result<int>>
    {
        private readonly IUniversityProfUnitContext _context;
        public UpdateAcademicDegreeCommandHandler(IUniversityProfUnitContext universityProfUnitContext)
        {
            _context = universityProfUnitContext;
        }

        public async Task<Result<int>> Handle(UpdateAcademicDegreeCommand request, CancellationToken cancellationToken)
        {
            Maybe<Logic.AcademicDegree> AcademicDegreeResult = 
                await _context.AcademicDegrees.FirstOrDefaultAsync(x => x.AcademicDegreeId == request.AcademicDegreeId);

            if (AcademicDegreeResult.HasNoValue)
                return Result.Failure<int>(Messages.AcademicDegreeNotFound);

            Logic.AcademicDegree AcademicDegree = AcademicDegreeResult.Value;

            Result<Logic.AcademicDegree> updateResult = AcademicDegree.UpdateAcademicDegree(request.AcademicDegreeName);

            if (updateResult.IsFailure)
                return Result.Failure<int>(updateResult.Error);

            var saveResult = await _context.SaveChangesWithValidation();

            if (saveResult.IsFailure)
                return Result.Failure<int>(saveResult.Error);

            return Result.Success(updateResult.Value.AcademicDegreeId);
        }
    }
}
