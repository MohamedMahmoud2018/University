using System.Collections.Generic;

namespace UniversityProfUnit.Application.KPI.Commands.CreateKPI
{
    public class KPIMainCategoryDto
    {
        public string KPIMainCategoryName { get; set; }
        public int KPIMainCategoryWehight { get; set; }
        public List<KPISupCategoryDto> KPISupCategoryList { get; set; }
    }
}