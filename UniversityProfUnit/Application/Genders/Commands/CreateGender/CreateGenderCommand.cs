using CSharpFunctionalExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniversityProfUnit.Logic;
using UniversityProfUnit.Logic.InterFaces;

namespace UniversityProfUnit.Application.Genders.Commands.CreateGender
{
    public class CreateGenderCommand : IRequest<Result<int>>
    {
        public string GenderName { get; set; }
    }

    public class CreateGenderCommandHandler : IRequestHandler<CreateGenderCommand, Result<int>>
    {
        private readonly IUniversityProfUnitContext _context;

        public CreateGenderCommandHandler(IUniversityProfUnitContext universityProfUnitContext)
        {
            _context = universityProfUnitContext;
        }
        public async Task<Result<int>> Handle(CreateGenderCommand request, CancellationToken cancellationToken)
        {
            var createResult = Gender.CreateGender(request.GenderName);

            if (createResult.IsFailure)
                return Result.Failure<int>(createResult.Error);

            _context.Genders.Add(createResult.Value);

            var savingResult = await _context.SaveChangesWithValidation();

            if (savingResult.IsFailure)
                return Result.Failure<int>(savingResult.Error);

            return Result.Success(createResult.Value.GenderId);
        }
    }
}
