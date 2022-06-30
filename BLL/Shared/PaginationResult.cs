using System.Collections.Generic;

namespace BLL
{
    public class PaginationResult<TEntity>
    {
        public IEnumerable<TEntity> Data { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}