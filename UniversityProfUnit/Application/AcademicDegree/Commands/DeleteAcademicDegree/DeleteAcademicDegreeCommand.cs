using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniversityProfUnit.Logic.InterFaces;

namespace UniversityProfUnit.Application.AcademicDegree.Commands.DeleteAcademicDegree
{
    public class DeleteAcademicDegreeCommand : IRequest<Result>
    {
        public int AcademicDegreeId { get; set; }
    }

    public class DeleteAcademicDegreeCommandHandler : IRequestHandler<DeleteAcademicDegreeCommand, Result>
    {
        private readonly IUniversityProfUnitContext _context;
        public DeleteAcademicDegreeCommandHandler(IUniversityProfUnitContext universityProfUnitContext)
        {
            _context = universityProfUnitContext;
        }

        public async Task<Result> Handle(DeleteAcademicDegreeCommand request, CancellationToken cancellationToken)
        {
            Maybe<Logic.AcademicDegree> AcademicDegreeResult = 
                await _context.AcademicDegrees.FirstOrDefaultAsync(x => x.AcademicDegreeId == request.AcademicDegreeId);

            if (AcademicDegreeResult.HasNoValue)
                return Result.Failure(Messages.AcademicDegreeNotFound);

            Logic.AcademicDegree academicDegree = AcademicDegreeResult.Value;

            List<int> usedIds = await _context.Profiles.SelectMany(x => x.ProfileAcademicDegreeList.Select(y => y.AcademicDegreeId)).ToListAsync();

            var validateDeleteResult = academicDegree.ValidateForDelete(usedIds);

            if (validateDeleteResult.IsFailure)
                return Result.Failure(validateDeleteResult.Error);

            var deleteResult = _context.AcademicDegrees.Remove(academicDegree);

            var saveResult = await _context.SaveChangesWithValidation();

            if (saveResult.IsFailure)
                return Result.Failure<int>(saveResult.Error);

            return Result.Success();
        }
    }
}
