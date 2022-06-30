using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProfUnit.Logic.ProfileAgreget.ProfileDtos;

namespace UniversityProfUnit.Logic.ProfileAgreget
{
    public class ProfileCourse
    {
        private ProfileCourse()
        {

        }
		public int ProfileCourseId { get; internal set; }
		public int ProfileId { get; internal set; }
		public string CourseName { get; internal set; }
		public DateTime FromDate { get; internal set; }
		public DateTime ToDate { get; internal set; }
		public string Description { get; internal set; }

        internal static Result<ProfileCourse> CreateProfileCourse(ProfileCoursesDto item)
        {
            var validateResult = ValidateCourse(item);

            if (validateResult.IsFailure)
                return Result.Failure<ProfileCourse>(validateResult.Error);

            ProfileCourse course = new ProfileCourse
            {
                CourseName = item.CourseName,
                Description = item.Description,
                FromDate = item.FromDate,
                ToDate = item.ToDate
            };

            return Result.Success(course);
        }

        public Result<ProfileCourse> UpdateProfileCourse(ProfileCoursesDto item)
        {
            var validateResult = ValidateCourse(item);

            if (validateResult.IsFailure)
                return Result.Failure<ProfileCourse>(validateResult.Error);

            CourseName = item.CourseName;
            Description = item.Description;
            FromDate = item.FromDate;
            ToDate = item.ToDate;

            return Result.Success(this);
        }

        private static Result ValidateCourse(ProfileCoursesDto course)
        {
            if (string.IsNullOrEmpty(course.CourseName))
                return Result.Failure(Messages.CourseNameRequired);

            if (course.ToDate == DateTime.MinValue || course.ToDate < course.FromDate)
                return Result.Failure(Messages.InvalidDate);

            return Result.Success();
        }
    }
}
