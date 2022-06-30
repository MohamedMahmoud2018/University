using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniversityProfUnit.Logic.InterFaces;
using UniversityProfUnit.Logic.ProfileAgreget;

namespace UniversityProfUnit.Application.Profiles.Commands.CreateProfile
{
    public class CreateProfileCommand : IRequest<Result<int>>
	{
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

    public class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand, Result<int>>
    {
        private readonly IUniversityProfUnitContext _context;
        public CreateProfileCommandHandler(IUniversityProfUnitContext universityProfUnitContext)
        {
            _context = universityProfUnitContext;
        }
        public async Task<Result<int>> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
        {
            Maybe<Logic.Gender> maybeGender = await _context.Genders.FirstOrDefaultAsync(x => x.GenderId == request.GenderId);

            Maybe<Logic.Specialty> maybeSpecialty = await _context.Specialties.FirstOrDefaultAsync(x => x.SpecialtyId == request.SpecialtyId);

            int maxSerial = await _context.Profiles.MaxAsync(x => (int?)x.Serial) ?? 0;

            List<string> emailList = await _context.Profiles.Select(x => x.Email).ToListAsync();
            List<string> phoneNumsList = await _context.Profiles.Select(x => x.Email).ToListAsync();

            var createResult = Profile.CreateProfile(
                request.FirstName,
                request.SecondName,
                request.Email,
                request.PhoneNumber,
                maybeSpecialty,
                request.PositionAbbreviation,
                request.CurrentJop,
                request.MainJob,
                maybeGender,
                maxSerial,
                phoneNumsList,
                emailList);

            if (createResult.IsFailure)
                return Result.Failure<int>(createResult.Error);

            _context.Profiles.Add(createResult.Value);

            var saveResult = await _context.SaveChangesWithValidation();

            if (saveResult.IsFailure)
                return Result.Failure<int>(saveResult.Error);

            return createResult.Value.ProfileId;
        }
    }
}
