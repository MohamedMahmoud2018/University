using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProfUnit.Logic.ProfileAgreget.ProfileDtos;

namespace UniversityProfUnit.Logic.ProfileAgreget
{
    public class ProfileExperience
    {
        private ProfileExperience()
        {

        }
		public int ProfileExperienceId { get; internal set; }
		public int ProfileId { get; internal set; }
		public string JopName { get; internal set; }
		public string Employer { get; internal set; }
		public DateTime FromDate { get; internal set; }
		public DateTime ToDate { get; internal set; }
		public string Description { get; internal set; }

        internal static Result<ProfileExperience> CreateProfileExperience(ProfileExperiencesDto item)
        {
            var validateResult = ValidateExperience(item);

            if (validateResult.IsFailure)
                return Result.Failure<ProfileExperience>(validateResult.Error);

            ProfileExperience experience = new ProfileExperience
            {
                Description = item.Description,
                Employer = item.Employer,
                FromDate = item.FromDate,
                JopName = item.JopName,
                ToDate = item.ToDate
            };

            return Result.Success(experience);
        }

        public Result<ProfileExperience> UpdateProfileExperience(ProfileExperiencesDto item)
        {
            var validateResult = ValidateExperience(item);

            if (validateResult.IsFailure)
                return Result.Failure<ProfileExperience>(validateResult.Error);

            Description = item.Description;
            Employer = item.Employer;
            FromDate = item.FromDate;
            JopName = item.JopName;
            ToDate = item.ToDate;

            return Result.Success(this);
        }

        private static Result ValidateExperience(ProfileExperiencesDto course)
        {
            if (string.IsNullOrEmpty(course.Employer))
                return Result.Failure(Messages.EmployerIsRequired);

            if (string.IsNullOrEmpty(course.JopName))
                return Result.Failure(Messages.JopNameIsRequired);

            if (course.ToDate == DateTime.MinValue || course.ToDate < course.FromDate)
                return Result.Failure(Messages.InvalidDate);

            return Result.Success();
        }
    }
}
