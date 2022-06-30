using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityProfUnit.Logic
{
    public class Gender
    {
        private Gender()
        {

        }
        public int GenderId { get; internal set; }
        public string GenderName { get; internal set; }

        public static Result<Gender> CreateGender(string name)
        {
            if (string.IsNullOrEmpty(name))
                return Result.Failure<Gender>(Messages.NameIsRequired);

            var gender = new Gender()
            {
                GenderName = name
            };

            return Result.Success(gender);
        }

        public Result<Gender> UpdateGender(string name)
        {
            if (string.IsNullOrEmpty(name))
                return Result.Failure<Gender>(Messages.NameIsRequired);

            GenderName = name;

            return Result.Success(this);
        }

        public Result ValidateForDelete(List<int> usedIds)
        {
            if (usedIds.Contains(GenderId))
                return Result.Failure(Messages.GenderCanNotDeleteIsUsed);

            return Result.Success();
        }
    }
}
