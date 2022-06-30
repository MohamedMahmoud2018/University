using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityProfUnit.Logic.ProfileKPIAgreget
{
    public class ProfileKPITemplate
    {
        private ProfileKPITemplate()
        {

        }
        public int ProfileKPITemplateId { get; internal set; }
        public int ProfileId { get; internal set; }
        public int KPITemplateId { get; internal set; }
        public List<ProfileKPIMainCategory> ProfileKPIMainCategoryList { get; set; }
    }
}
