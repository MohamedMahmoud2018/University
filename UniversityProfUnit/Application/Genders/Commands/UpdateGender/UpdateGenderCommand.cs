using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniversityProfUnit.Logic.InterFaces;

namespace UniversityProfUnit.Application.Genders.Commands.UpdateGenders
{
    public class UpdateGenderCommand : IRequest<Result<int>>
    {
        public int GenderId { get; set; }
        public string GenderName { get; set; }
    }

    public class UpdateGenderCommandHandler : IRequestHandler<UpdateGenderCommand, Result<int>>
    {
        private readonly IUniversityProfUnitContext _context;
        public UpdateGenderCommandHandler(IUniversityProfUnitContext universityProfUnitContext)
        {
            _context = universityProfUnitContext;
        }

        public async Task<Result<int>> Handle(UpdateGenderCommand request, CancellationToken cancellationToken)
        {
            Maybe<Logic.Gender> GenderResult = await _context.Genders.FirstOrDefaultAsync(x => x.GenderId == request.GenderId);

            if (GenderResult.HasNoValue)
                return Result.Failure<int>(Messages.GenderNotFound);

            Logic.Gender Gender = GenderResult.Value;

            Result<Logic.Gender> updateResult = Gender.UpdateGender(request.GenderName);

            if (updateResult.IsFailure)
                return Result.Failure<int>(updateResult.Error);

            var saveResult = await _context.SaveChangesWithValidation();

            if (saveResult.IsFailure)
                return Result.Failure<int>(saveResult.Error);

            return Result.Success( updateResult.Value.GenderId);
        }
    }
}
