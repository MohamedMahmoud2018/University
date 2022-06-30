using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniversityProfUnit.Logic.InterFaces;

namespace UniversityProfUnit.Application.Genders.Commands.DeleteGender
{
    public class DeleteGenderCommand : IRequest<Result>
    {
        public int GenderId { get; set; }
    }

    public class DeleteGenderCommandHandler : IRequestHandler<DeleteGenderCommand, Result>
    {
        private readonly IUniversityProfUnitContext _context;
        public DeleteGenderCommandHandler(IUniversityProfUnitContext universityProfUnitContext)
        {
            _context = universityProfUnitContext;
        }

        public async Task<Result> Handle(DeleteGenderCommand request, CancellationToken cancellationToken)
        {
            Maybe<Logic.Gender> GenderResult = await _context.Genders.FirstOrDefaultAsync(x => x.GenderId == request.GenderId);

            if (GenderResult.HasNoValue)
                return Result.Failure(Messages.GenderNotFound);

            Logic.Gender gender = GenderResult.Value;

            List<int> usedIds = await _context.Profiles.Select(x => x.GenderId).ToListAsync();

            var validateDeleteResult = gender.ValidateForDelete(usedIds);

            if (validateDeleteResult.IsFailure)
                return Result.Failure(validateDeleteResult.Error);

            var deleteResult = _context.Genders.Remove(gender);

            var saveResult = await _context.SaveChangesWithValidation();

            if (saveResult.IsFailure)
                return Result.Failure<int>(saveResult.Error);

            return Result.Success();
        }
    }
}
