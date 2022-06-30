using CSharpFunctionalExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UniversityProfUnit.Logic.InterFaces;
using UniversityProfUnit.Logic.KPIAgreget;

namespace UniversityProfUnit.Application.KPI.Commands.CreateKPI
{
    public class CreateKPICommand : IRequest<Result<int>>
    {
        public string KPITemplateName { get; set; }
        public List<KPIMainCategoryDto> KPIMainCategoryList { get; set; }
    }

    public class CreateKPICommandHandler : IRequestHandler<CreateKPICommand, Result<int>>
    {
        private readonly IUniversityProfUnitContext _context;

        public CreateKPICommandHandler(IUniversityProfUnitContext universityProfUnitContext)
        {
            _context = universityProfUnitContext;
        }
        public async Task<Result<int>> Handle(CreateKPICommand request, CancellationToken cancellationToken)
        {
            List<Logic.KPIAgreget.KPIDtos.KPIMainCategoryDto> kPIMainCategories = new List<Logic.KPIAgreget.KPIDtos.KPIMainCategoryDto>();

            foreach (var item in request.KPIMainCategoryList)
            {
                kPIMainCategories.Add(new Logic.KPIAgreget.KPIDtos.KPIMainCategoryDto()
                {
                    KPIMainCategoryName = item.KPIMainCategoryName,
                    KPIMainCategoryWehight = item.KPIMainCategoryWehight,
                    KPISupCategoryList = item.KPISupCategoryList.Select(x => new Logic.KPIAgreget.KPIDtos.KPISupCategoryDto()
                    {
                        KPISupCategoryName = x.KPISupCategoryName,
                        KPISupCategoryWeight = x.KPISupCategoryWeight
                    }).ToList()
                });
            }

            var createResult = KPITemplate.CreateKPITemplate(request.KPITemplateName, kPIMainCategories);

            if (createResult.IsFailure)
                return Result.Failure<int>(createResult.Error);

            _context.KPITemplates.Add(createResult.Value);

            var saveResult = await _context.SaveChangesWithValidation();

            if (saveResult.IsFailure)
                return Result.Failure<int>(saveResult.Error);

            return createResult.Value.KPITemplateId;
        }
    }
}
