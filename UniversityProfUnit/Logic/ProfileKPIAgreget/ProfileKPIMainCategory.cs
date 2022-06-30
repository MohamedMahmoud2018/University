using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityProfUnit.Logic.ProfileKPIAgreget
{
    public class ProfileKPIMainCategory
    {
        private ProfileKPIMainCategory()
        {

        }
        public int ProfileKPIMainCategoryId { get; internal set; }
        public int KPIMainCategoryId { get; internal set; }
        public int ProfileKPITemplateId { get; internal set; }
        public List<ProfileKPISubCategory> ProfileKPISubCategoryList { get; set; }
    }
}
