using System;
using System.Collections.Generic;

namespace Practical.Models
{
    public partial class Pagination<T>
    {
        public int Id { get; set; }
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public List<T> Items { get; set; } 

        public Pagination(List<T> items, int count, int pageIndex, int pageSize)
        {
            Items = items;
            TotalCount = count;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public bool HasPreviousPage => (PageIndex > 1);
        public bool HasNextPage => (PageIndex < TotalPages);
    }
}
