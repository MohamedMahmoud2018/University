using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProfUnit.Logic.KPIAgreget.KPIDtos;

namespace UniversityProfUnit.Logic.KPIAgreget
{
    public class KPIMainCategory
    {
        private KPIMainCategory()
        {

        }
        public int KPIMainCategoryId { get; internal set; }
        public int KPITemplateId { get; internal set; }
        public string KPIMainCategoryName { get; internal set; }
        public int KPIMainCategoryWehight { get; internal set; }
        public List<KPISupCategory> KPISupCategoryList { get; internal set; }

        internal static Result<KPIMainCategory> CreateKPIMainCategory(KPIMainCategoryDto mainCategory)
        {
            var validateResult = ValidateKPIMainCategory(mainCategory);

            if (validateResult.IsFailure)
                return Result.Failure<KPIMainCategory>(validateResult.Error);

            List<KPISupCategory> subCategories = new List<KPISupCategory>();

            foreach (var item in mainCategory.KPISupCategoryList)
            {
                var createResult = KPISupCategory.CreateKPISubCategory(item);

                if (createResult.IsFailure)
                    return Result.Failure<KPIMainCategory>(createResult.Error);

                subCategories.Add(createResult.Value);
            }

            if (subCategories.Sum(x => x.KPISupCategoryWeight) != 100)
                return Result.Failure<KPIMainCategory>(Messages.SumOfWehightMustBeOneHundered);

            KPIMainCategory kPITemplate = new KPIMainCategory()
            {
                KPIMainCategoryName = mainCategory.KPIMainCategoryName,
                KPIMainCategoryWehight = mainCategory.KPIMainCategoryWehight,
                KPISupCategoryList = subCategories
            };

            return Result.Success(kPITemplate);
        }

        private static Result ValidateKPIMainCategory(KPIMainCategoryDto mainCategory)
        {
            if (string.IsNullOrEmpty(mainCategory.KPIMainCategoryName))
                return Result.Failure(Messages.NameIsRequired);

            if (mainCategory.KPISupCategoryList.Count <= 0)
                return Result.Failure(Messages.InsertKPISubCategory);

            return Result.Success();
        }
    }
}
