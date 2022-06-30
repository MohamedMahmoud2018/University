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
using UniversityProfUnit.Logic.ProfileAgreget;

namespace UniversityProfUnit.Application.Profiles.Commands.UpdateProfileMain
{
    public class UpdateProfileMainCommand : IRequest<Result<int>>
    {
        public int ProfileId { get; set; }
        public string FirstName { get; set; }
		public string SecondName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public int SpecialtyId { get; set; }
		public string PositionAbbreviation { get; set; }
		public string CurrentJop { get; set; }
		public string MainJob { get; set; }
		public int GenderId { get; set; }
	}

    public class UpdateProfileMainCommandHandler : IRequestHandler<UpdateProfileMainCommand, Result<int>>
    {
        private readonly IUniversityProfUnitContext _context;
        public UpdateProfileMainCommandHandler(IUniversityProfUnitContext universityProfUnitContext)
        {
            _context = universityProfUnitContext;
        }
        public async Task<Result<int>> Handle(UpdateProfileMainCommand request, CancellationToken cancellationToken)
        {
            Maybe<Profile> maybeProfile = await _context.Profiles.FirstOrDefaultAsync(x => x.ProfileId == request.ProfileId);

            if (maybeProfile.HasNoValue)
                return Result.Failure<int>(Messages.ProfileNotFound);

            Profile profile = maybeProfile.Value;

            Maybe<Logic.Gender> maybeGender = await _context.Genders.FirstOrDefaultAsync(x => x.GenderId == request.GenderId);

            Maybe<Logic.Specialty> maybeSpecialty = await _context.Specialties.FirstOrDefaultAsync(x => x.SpecialtyId == request.SpecialtyId);

            List<string> emailList = await _context.Profiles.Select(x => x.Email).ToListAsync();
            List<string> phoneNumsList = await _context.Profiles.Select(x => x.Email).ToListAsync();

            var updateResult = profile.UpdateProfileMain(
                request.FirstName,
                request.SecondName,
                request.Email,
                request.PhoneNumber,
                maybeSpecialty,
                request.PositionAbbreviation,
                request.CurrentJop,
                request.MainJob,
                maybeGender,
                phoneNumsList,
                emailList);

            if (updateResult.IsFailure)
                return Result.Failure<int>(updateResult.Error);

            var saveResult = await _context.SaveChangesWithValidation();

            if (saveResult.IsFailure)
                return Result.Failure<int>(saveResult.Error);

            return updateResult.Value.ProfileId;
        }
    }
}
