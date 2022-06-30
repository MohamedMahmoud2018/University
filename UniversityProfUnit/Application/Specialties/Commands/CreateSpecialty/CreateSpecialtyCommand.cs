using CSharpFunctionalExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniversityProfUnit.Logic;
using UniversityProfUnit.Logic.InterFaces;

namespace UniversityProfUnit.Application.Specialties.Commands.CreateSpecialty
{   
    public class CreateSpecialtyCommand : IRequest<Result<int>>
    {
        public string SpecialtyName { get; set; }
    }

    public class CreateSpecialtyCommandHandler : IRequestHandler<CreateSpecialtyCommand, Result<int>>
    {
        private readonly IUniversityProfUnitContext _context;

        public CreateSpecialtyCommandHandler(IUniversityProfUnitContext universityProfUnitContext)
        {
            _context = universityProfUnitContext;
        }
        public async Task<Result<int>> Handle(CreateSpecialtyCommand request, CancellationToken cancellationToken)
        {
            var creatResult = Specialty.CreateSpecialty(request.SpecialtyName);

            if (creatResult.IsFailure)
                return Result.Failure<int>(creatResult.Error);

            _context.Specialties.Add(creatResult.Value);

            var saveResult = await _context.SaveChangesWithValidation();

            if (saveResult.IsFailure)
                return Result.Failure<int>(saveResult.Error);

            return Result.Success( creatResult.Value.SpecialtyId);
        }
    }
}
