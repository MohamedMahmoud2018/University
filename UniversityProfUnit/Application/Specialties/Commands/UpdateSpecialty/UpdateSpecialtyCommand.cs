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

namespace UniversityProfUnit.Application.Specialties.Commands.UpdateSpecialty
{
    public class UpdateSpecialtyCommand : IRequest<Result<int>>
    {
        public int SpecialtyId { get; set; }
        public string SpecialtyName { get; set; }
    }

    public class UpdateSpecialtyCommandHandler : IRequestHandler<UpdateSpecialtyCommand, Result<int>>
    {
        private readonly IUniversityProfUnitContext _context;

        public UpdateSpecialtyCommandHandler(IUniversityProfUnitContext universityProfUnitContext)
        {
            _context = universityProfUnitContext;
        }

        public async Task<Result<int>> Handle(UpdateSpecialtyCommand request, CancellationToken cancellationToken)
        {
            Maybe<Specialty> specialtyResult = await _context.Specialties.FirstOrDefaultAsync(x => x.SpecialtyId == request.SpecialtyId);

            if (specialtyResult.HasNoValue)
                return Result.Failure<int>(Messages.SpecialtyNotFound);

            Specialty specialty = specialtyResult.Value;

            Result<Specialty> updateResult = specialty.UpdateSpecialty(request.SpecialtyName);

            if (updateResult.IsFailure)
                return Result.Failure<int>(updateResult.Error);

            var saveResult = await _context.SaveChangesWithValidation();

            if (saveResult.IsFailure)
                return Result.Failure<int>(saveResult.Error);

            return Result.Success( updateResult.Value.SpecialtyId);
        }
    }
}
