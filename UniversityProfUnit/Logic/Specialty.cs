using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityProfUnit.Logic
{
    public class Specialty
    {
        private Specialty()
        {

        }
        public int SpecialtyId { get; internal set; }
        public string SpecialtyName { get; internal set; }

        public static Result<Specialty> CreateSpecialty(string name)
        {
            if (string.IsNullOrEmpty(name))
                return Result.Failure<Specialty>(Messages.NameIsRequired);

            var specialty = new Specialty()
            {
                SpecialtyName = name
            };

            return Result.Success(specialty);
        }

        public Result<Specialty> UpdateSpecialty(string name)
        {
            if (string.IsNullOrEmpty(name))
                return Result.Failure<Specialty>(Messages.NameIsRequired);

            SpecialtyName = name;

            return Result.Success(this);
        }

        public Result ValidateForDelete(List<int> usedIds)
        {
            if (usedIds.Contains(SpecialtyId))
                return Result.Failure(Messages.SpecialtyCanNotDeleteIsUsed);

            return Result.Success();
        }
    }
}
