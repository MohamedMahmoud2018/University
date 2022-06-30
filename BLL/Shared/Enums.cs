
using System.Collections.Generic;

namespace BLL
{
    public class Enums
    {
        public enum Languages
        {
            AR = 1,
            EN = 2
        }
        public static class LanguagesStr
        {
            public static string AR { get; set; } = "AR";
            public static string EN { get; set; } = "EN";
        }
        public static List<object> SearchKeys = new List<object> 
        { 
            new{EN="Speciality" ,AR="التخصص"},
            new{EN="Gender",AR="النوع" },
            new{EN="PositionAbbreviation" ,AR="مختصر الخبرة/المنصب"},
            new{EN="University" ,AR="جهة العمل"},
            new{EN="MainJob",AR="الوظيفة الأساسية" },
            new{EN="DiscoveryChannel",AR="قناة الاستكشاف" },
            new{EN="AcademicRank",AR="الرتبة العلمية" }
        };
    }
    
}
