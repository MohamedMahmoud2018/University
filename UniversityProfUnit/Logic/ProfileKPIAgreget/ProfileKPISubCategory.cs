using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityProfUnit.Logic.ProfileKPIAgreget
{
    public class ProfileKPISubCategory
    {
        private ProfileKPISubCategory()
        {

        }
        public int ProfileKPISubCategoryId { get; internal set; }
        public int ProfileKPIMainCategoryId { get; internal set; }
        public int KPISupCategoryId { get; internal set; }
        public int KPIEvaluationId { get; internal set; }
    }
}
