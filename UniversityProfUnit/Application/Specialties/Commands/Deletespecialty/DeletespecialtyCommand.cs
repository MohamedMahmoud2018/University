using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniversityProfUnit.Logic;
using UniversityProfUnit.Logic.InterFaces;

namespace UniversityProfUnit.Application.Specialties.Commands.Deletespecialty
{
    public class DeletespecialtyCommand : IRequest<Result>
    {
        public int SpecialtyId { get; set; }
    }

    public class DeletespecialtyCommandHandler : IRequestHandler<DeletespecialtyCommand, Result>
    {
        private readonly IUniversityProfUnitContext _context;
        public DeletespecialtyCommandHandler(IUniversityProfUnitContext universityProfUnitContext)
        {
            _context = universityProfUnitContext;
        }

        public async Task<Result> Handle(DeletespecialtyCommand request, CancellationToken cancellationToken)
        {
            Maybe<Specialty> specialtyResult = await _context.Specialties.FirstOrDefaultAsync(x => x.SpecialtyId == request.SpecialtyId);

            if (specialtyResult.HasNoValue)
                return Result.Failure(Messages.GenderNotFound);

            Specialty specialty = specialtyResult.Value;

            List<int> usedIds = await _context.Profiles.Select(x => x.SpecialtyId).ToListAsync();

            var validateDeleteResult = specialty.ValidateForDelete(usedIds);

            if (validateDeleteResult.IsFailure)
                return Result.Failure(validateDeleteResult.Error);

            var deleteResult = _context.Specialties.Remove(specialty);

            var saveResult = await _context.SaveChangesWithValidation();

            if (saveResult.IsFailure)
                return Result.Failure<int>(saveResult.Error);

            return Result.Success();
        }
    }
}
