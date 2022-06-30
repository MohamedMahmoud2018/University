using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProfUnit.Logic.ProfileAgreget.ProfileDtos;

namespace UniversityProfUnit.Logic.ProfileAgreget
{
    public class Profile
    {
        private Profile()
        {

        }
        public int ProfileId { get; internal set; }
        public int Serial { get; internal set; }
        public string FirstName { get; internal set; }
        public string SecondName { get; internal set; }
        public string Email { get; internal set; }
        public string PhoneNumber { get; internal set; }
        public int SpecialtyId { get; internal set; }
        public string PositionAbbreviation { get; internal set; }
        public string CurrentJop { get; internal set; }
        public string MainJob { get; internal set; }
        public int GenderId { get; internal set; }
        public int? DiscoveryChannelId { get; internal set; }
        public string Description { get; internal set; }
        public bool ProfileState { get; internal set; }
        public List<ProfileAcademicDegree> ProfileAcademicDegreeList { get; internal set; } = new List<ProfileAcademicDegree>();
        public List<ProfileCourse> ProfileCourseList { get; internal set; } = new List<ProfileCourse>();
        public List<ProfileExperience> ProfileExperienceList { get; internal set; } = new List<ProfileExperience>();

        internal static Result<Profile> CreateProfile(
                string firstName,
                string secondName,
                string email,
                string phoneNumber,
                Maybe<Logic.Specialty> maybeSpecialty,
                string positionAbbreviation,
                string currentJop,
                string mainJob,
                Maybe<Logic.Gender> maybeGender,
                int maxSerial,
                List<string> phoneNumberlList,
                List<string> emailList
            )
        {
            string validEmail = email.Trim().Replace(" ", "").ToLower();

            string validPhoneNumber = phoneNumber.Trim().Replace(" ", "");

            var validationResult = ValidateProfile(
                maybeSpecialty,
                maybeGender,
                validEmail,
                emailList,
                validPhoneNumber,
                phoneNumberlList,
                firstName,
                secondName,
                positionAbbreviation,
                currentJop,
                mainJob);

            if (validationResult.IsFailure)
                return Result.Failure<Profile>(validationResult.Error);

            Profile profile = new Profile
            {
                FirstName = firstName,
                SecondName = secondName,
                Email = validEmail,
                PhoneNumber = validPhoneNumber,
                SpecialtyId = maybeSpecialty.Value.SpecialtyId,
                PositionAbbreviation = positionAbbreviation,
                CurrentJop = currentJop,
                MainJob = mainJob,
                GenderId = maybeGender.Value.GenderId,
                Serial = maxSerial + 1
            };

            return Result.Success(profile);
        }

        private static Result ValidateProfile (
            Maybe<Logic.Specialty> maybeSpecialty,
            Maybe<Logic.Gender> maybeGender,
            string validEmail,
            List<string> emailList,
            string validPhoneNumber,
             List<string> phoneNumberlList,
             string firstName,
             string secondName,
             string positionAbbreviation,
             string currentJop,
             string mainJob
            )
        {
            if (maybeSpecialty.HasNoValue)
                return Result.Failure<Profile>(Messages.SpecialtyNotFound);

            if (maybeGender.HasNoValue)
                return Result.Failure<Profile>(Messages.GenderNotFound);

            if (string.IsNullOrEmpty(firstName))
                return Result.Failure<Profile>(Messages.FirstNameRequired);

            if (string.IsNullOrEmpty(secondName))
                return Result.Failure<Profile>(Messages.SecondNameRequired);

            if (string.IsNullOrEmpty(validEmail))
                return Result.Failure<Profile>(Messages.EmailRequired);

            if (string.IsNullOrEmpty(validPhoneNumber))
                return Result.Failure<Profile>(Messages.PhoneNumberRequired);

            if (string.IsNullOrEmpty(positionAbbreviation))
                return Result.Failure<Profile>(Messages.PositionAbbreviationRequired);

            if (string.IsNullOrEmpty(currentJop))
                return Result.Failure<Profile>(Messages.CurrentJopRequired);

            if (string.IsNullOrEmpty(mainJob))
                return Result.Failure<Profile>(Messages.MainJobRequired);

            if (emailList.Contains(validEmail))
                return Result.Failure<Profile>(Messages.DuplicatedEmail);

            int outRes = default;

            if (!int.TryParse(validPhoneNumber, out outRes))
                return Result.Failure<Profile>(Messages.WrongPhoneNumber);

            if (validPhoneNumber.Length != 12)
                return Result.Failure<Profile>(Messages.WrongPhoneNumber);

            if (phoneNumberlList.Contains(validPhoneNumber))
                return Result.Failure<Profile>(Messages.DublicatedPhoneNumber);

            return Result.Success();
        }

        public Result<Profile> UpdateProfileAdditionalInfo(Maybe<DiscoveryChannel> maybeDiscoveryChannel, string description)
        {
            if (maybeDiscoveryChannel.HasNoValue)
                return Result.Failure<Profile>(Messages.DiscoveryChannelNotFound);

            DiscoveryChannelId = maybeDiscoveryChannel.Value.DiscoveryChannelId;
            Description = description;
            ProfileState = true;

            return Result.Success(this);
        }

        public Result<Profile> UpdateProfileCourses(List<ProfileCoursesDto> coursesDtos)
        {
            var removedItemsIds = ProfileCourseList
                 .Select(x => x.ProfileCourseId)
                 .Where(y => !coursesDtos.Select(t => t.ProfileCourseId).Contains(y));

            ProfileCourseList.RemoveAll(x => removedItemsIds.Contains(x.ProfileCourseId));

            foreach (var item in coursesDtos)
            {
                if (item.ProfileCourseId == 0)
                {
                    var creatResult = ProfileCourse.CreateProfileCourse(item);

                    if (creatResult.IsFailure)
                        return Result.Failure<Profile>(creatResult.Error);

                    ProfileCourseList.Add(creatResult.Value);
                }
                else
                {
                    var profileCourse = ProfileCourseList.FirstOrDefault(x => x.ProfileCourseId == item.ProfileCourseId);

                    var updateResult = profileCourse.UpdateProfileCourse(item);

                    if (updateResult.IsFailure)
                        return Result.Failure<Profile>(updateResult.Error);
                }
            }

            if (ProfileCourseList.Count < 3)
                return Result.Failure<Profile>(Messages.CourseListMustBeAtLeastThree);

            return Result.Success(this);
        }

        public Result<Profile> UpdateProfileExperiences(List<ProfileExperiencesDto> experiencesDtos)
        {
            var removedItemsIds = ProfileExperienceList
                .Select(x => x.ProfileExperienceId)
                .Where(y => !experiencesDtos.Select(t => t.ProfileExperienceId).Contains(y));

            ProfileExperienceList.RemoveAll(x => removedItemsIds.Contains(x.ProfileExperienceId));

            foreach (var item in experiencesDtos)
            {
                if (item.ProfileExperienceId == 0)
                {
                    var creatResult = ProfileExperience.CreateProfileExperience(item);

                    if (creatResult.IsFailure)
                        return Result.Failure<Profile>(creatResult.Error);

                    ProfileExperienceList.Add(creatResult.Value);
                }
                else
                {
                    var profileExperience = ProfileExperienceList.FirstOrDefault(x => x.ProfileExperienceId == item.ProfileExperienceId);

                    var updateResult = profileExperience.UpdateProfileExperience(item);

                    if (updateResult.IsFailure)
                        return Result.Failure<Profile>(updateResult.Error);
                }
            }

            if (ProfileCourseList.Count < 3)
                return Result.Failure<Profile>(Messages.CourseListMustBeAtLeastThree);

            return Result.Success(this);
        }

        public Result<Profile> UpdateProfileAcademicDegree(List<ProfileAcademicDegreeDto> academicDegreeDtos)
        {
            var removedItemsIds = ProfileAcademicDegreeList
                .Select(x => x.ProfileAcademicDegreeId)
                .Where(y => !academicDegreeDtos.Select(t => t.ProfileAcademicDegreeId).Contains(y));

            ProfileAcademicDegreeList.RemoveAll(x => removedItemsIds.Contains(x.ProfileAcademicDegreeId));

            foreach (var item in academicDegreeDtos)
            {
                if (item.ProfileAcademicDegreeId == 0)
                {
                    var creatResult = ProfileAcademicDegree.CreateProfileAcademicDegree(item);

                    if (creatResult.IsFailure)
                        return Result.Failure<Profile>(creatResult.Error);

                    ProfileAcademicDegreeList.Add(creatResult.Value);
                }
                else
                {
                    var academicDegree = ProfileAcademicDegreeList.FirstOrDefault(x => x.ProfileAcademicDegreeId == item.ProfileAcademicDegreeId);

                    var updateResult = academicDegree.UpdateProfileAcademicDegree(item);

                    if (updateResult.IsFailure)
                        return Result.Failure<Profile>(updateResult.Error);
                }
            }

            return Result.Success(this);
        }

        public Result<Profile> UpdateProfileMain(
            string firstName,
                string secondName,
                string email,
                string phoneNumber,
                Maybe<Logic.Specialty> maybeSpecialty,
                string positionAbbreviation,
                string currentJop,
                string mainJob,
                Maybe<Logic.Gender> maybeGender,
                List<string> phoneNumberlList,
                List<string> emailList
                )
        {
            string validEmail = email.Trim().Replace(" ", "").ToLower();

            string validPhoneNumber = phoneNumber.Trim().Replace(" ", "");
            
            var validationResult = ValidateProfile(
                maybeSpecialty,
                maybeGender,
                validEmail,
                emailList,
                validPhoneNumber,
                phoneNumberlList,
                firstName,
                secondName,
                positionAbbreviation,
                currentJop,
                mainJob);

            if (validationResult.IsFailure)
                return Result.Failure<Profile>(validationResult.Error);

            FirstName = firstName;
            SecondName = secondName;
            Email = validEmail;
            PhoneNumber = validPhoneNumber;
            SpecialtyId = maybeSpecialty.Value.SpecialtyId;
            PositionAbbreviation = positionAbbreviation;
            CurrentJop = currentJop;
            MainJob = mainJob;
            GenderId = maybeGender.Value.GenderId;

            return Result.Success(this);
        }
    }
}
