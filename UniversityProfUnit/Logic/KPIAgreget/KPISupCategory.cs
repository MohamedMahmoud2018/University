using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProfUnit.Logic.KPIAgreget.KPIDtos;

namespace UniversityProfUnit.Logic.KPIAgreget
{
    public class KPISupCategory
    {
        private KPISupCategory()
        {

        }
        public int KPISupCategoryId { get; internal set; }
        public int KPIMainCategoryId { get; internal set; }
        public string KPISupCategoryName { get; internal set; }
        public int KPISupCategoryWeight { get; internal set; }

        internal static Result<KPISupCategory> CreateKPISubCategory(KPISupCategoryDto item)
        {
            var validateResult = ValidateKPISupCategory(item);

            if (validateResult.IsFailure)
                return Result.Failure<KPISupCategory>(validateResult.Error);

            KPISupCategory kPISupCategory = new KPISupCategory()
            {
                KPISupCategoryName = item.KPISupCategoryName,
                KPISupCategoryWeight = item.KPISupCategoryWeight
            };

            return Result.Success(kPISupCategory);
        }

        private static Result ValidateKPISupCategory(KPISupCategoryDto subCat)
        {
            if (string.IsNullOrEmpty(subCat.KPISupCategoryName))
                return Result.Failure(Messages.NameIsRequired);

            return Result.Success();
        }
    }
}
