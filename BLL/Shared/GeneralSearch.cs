namespace BLL.Shared
{
    public class GeneralSearch
    {
        public int? StartRow { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string SearchText { get; set; }
        public int? LanguageId { get; set; }
    }
}