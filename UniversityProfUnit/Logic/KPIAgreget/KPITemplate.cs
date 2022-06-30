using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProfUnit.Logic.KPIAgreget.KPIDtos;

namespace UniversityProfUnit.Logic.KPIAgreget
{
    public class KPITemplate
    {
        private KPITemplate()
        {

        }
        public int KPITemplateId { get;internal set; }
        public string KPITemplateName { get; internal set; }
        public List<KPIMainCategory> KPIMainCategoryList { get; internal set; }

        internal static Result<KPITemplate> CreateKPITemplate(string name,List<KPIMainCategoryDto> kPIMainCategoryList)
        {
            var validateResult = ValidateKPITemplate(name, kPIMainCategoryList);

            if (validateResult.IsFailure)
                return Result.Failure<KPITemplate>(validateResult.Error);

            List<KPIMainCategory> mainCategories = new List<KPIMainCategory>();

            foreach (var item in kPIMainCategoryList)
            {
                var createResult = KPIMainCategory.CreateKPIMainCategory(item);

                if (createResult.IsFailure)
                    return Result.Failure<KPITemplate>(createResult.Error);

                mainCategories.Add(createResult.Value);
            }

            if (mainCategories.Sum(x => x.KPIMainCategoryWehight) != 100)
                return Result.Failure<KPITemplate>(Messages.SumOfWehightMustBeOneHundered);

            KPITemplate kPITemplate = new KPITemplate()
            {
                KPITemplateName = name,
                KPIMainCategoryList = mainCategories
            };

            return Result.Success(kPITemplate);
        }

        private static Result ValidateKPITemplate(string name, List<KPIMainCategoryDto> kPIMainCategoryList)
        {
            if (string.IsNullOrEmpty(name))
                return Result.Failure(Messages.NameIsRequired);

            if (kPIMainCategoryList.Count <= 0)
                return Result.Failure(Messages.InsertKPIMainCategory);

            return Result.Success();
        }
    }
}
