using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityProfUnit.Logic
{
    public class AcademicDegree
    {
        private AcademicDegree()
        {

        }
        public int AcademicDegreeId { get; internal set; }
        public string AcademicDegreeName { get; internal set; }

        public static Result<AcademicDegree> CreateAcademicDegree(string name)
        {
            if (string.IsNullOrEmpty(name))
                return Result.Failure<AcademicDegree>(Messages.NameIsRequired);

            var AcademicDegree = new AcademicDegree()
            {
                AcademicDegreeName = name
            };

            return Result.Success(AcademicDegree);
        }

        public Result<AcademicDegree> UpdateAcademicDegree(string name)
        {
            if (string.IsNullOrEmpty(name))
                return Result.Failure<AcademicDegree>(Messages.NameIsRequired);

            AcademicDegreeName = name;

            return Result.Success(this);
        }

        public Result ValidateForDelete(List<int> usedIds)
        {
            if (usedIds.Contains(AcademicDegreeId))
                return Result.Failure(Messages.AcademicDegreeCanNotDeleteIsUsed);

            return Result.Success();
        }
    }
}
