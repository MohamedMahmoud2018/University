using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProfUnit.Logic.ProfileAgreget.ProfileDtos;

namespace UniversityProfUnit.Logic.ProfileAgreget
{
    public class ProfileAcademicDegree
    {
        private ProfileAcademicDegree()
        {

        }
        public int ProfileAcademicDegreeId { get; internal set; }
        public int ProfileId { get; internal set; }
        public int AcademicDegreeId { get; internal set; }
        public string Specialty { get; internal set; }
        public string University { get; internal set; }
        public DateTime DegreeDate { get; internal set; }
        public AcademicDegree AcademicDegree { get; internal set; }

        internal static Result<ProfileAcademicDegree> CreateProfileAcademicDegree(ProfileAcademicDegreeDto academicDegreeDto)
        {
            var validateResult = ValidateAcademicDegree(academicDegreeDto);

            if (validateResult.IsFailure)
                return Result.Failure<ProfileAcademicDegree>(validateResult.Error);

            ProfileAcademicDegree degree = new ProfileAcademicDegree
            {
                Specialty = academicDegreeDto.Specialty,
                University = academicDegreeDto.University,
                DegreeDate = academicDegreeDto.DegreeDate,
                AcademicDegreeId = academicDegreeDto.AcademicDegree.Value.AcademicDegreeId
            };

            return Result.Success(degree);
        }

        public Result<ProfileAcademicDegree> UpdateProfileAcademicDegree(ProfileAcademicDegreeDto academicDegreeDto)
        {
            var validateResult = ValidateAcademicDegree(academicDegreeDto);

            if (validateResult.IsFailure)
                return Result.Failure<ProfileAcademicDegree>(validateResult.Error);

            Specialty = academicDegreeDto.Specialty;
            University = academicDegreeDto.University;
            DegreeDate = academicDegreeDto.DegreeDate;
            AcademicDegreeId = academicDegreeDto.AcademicDegree.Value.AcademicDegreeId;

            return Result.Success(this);
        }

        private static Result ValidateAcademicDegree(ProfileAcademicDegreeDto academicDegreeDto)
        {
            if (academicDegreeDto.AcademicDegree.HasNoValue)
                return Result.Failure<ProfileAcademicDegree>(Messages.AcademicDegreeNotFound);

            if (academicDegreeDto.DegreeDate == DateTime.MinValue)
                return Result.Failure(Messages.InvalidDate);

            if (string.IsNullOrEmpty(academicDegreeDto.Specialty))
                return Result.Failure(Messages.SpecialtyIsRequired);

            if (string.IsNullOrEmpty(academicDegreeDto.University))
                return Result.Failure(Messages.UniversityIsRequired);

            return Result.Success();
        }
    }
}
