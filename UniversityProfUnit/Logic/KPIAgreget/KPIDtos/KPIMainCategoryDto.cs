using System.Collections.Generic;

namespace UniversityProfUnit.Logic.KPIAgreget.KPIDtos
{
    public class KPIMainCategoryDto
    {
        public string KPIMainCategoryName { get; set; }
        public int KPIMainCategoryWehight { get; set; }
        public List<KPISupCategoryDto> KPISupCategoryList { get; set; }
    }
}