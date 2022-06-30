using CSharpFunctionalExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniversityProfUnit.Logic.InterFaces;

namespace UniversityProfUnit.Application.AcademicDegree.Commands.CreateAcademicDegree
{
    public class CreateAcademicDegreeCommand : IRequest<Result<int>>
    {
        public string AcademicDegreeName { get; set; }
    }

    public class CreateAcademicDegreeCommandHandler : IRequestHandler<CreateAcademicDegreeCommand, Result<int>>
    {
        private readonly IUniversityProfUnitContext _context;
        public CreateAcademicDegreeCommandHandler(IUniversityProfUnitContext universityProfUnitContext)
        {
            _context = universityProfUnitContext;
        }
        public async Task<Result<int>> Handle(CreateAcademicDegreeCommand request, CancellationToken cancellationToken)
        {
            var createResult = Logic.AcademicDegree.CreateAcademicDegree(request.AcademicDegreeName);

            if (createResult.IsFailure)
                return Result.Failure<int>(createResult.Error);

            _context.AcademicDegrees.Add(createResult.Value);

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success(createResult.Value.AcademicDegreeId);
        }
    }
}
