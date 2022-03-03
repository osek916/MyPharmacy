using System;
using System.Collections.Generic;

namespace MyPharmacy.Models
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public int CountOfPages { get; set; }
        public int ItemsFrom { get; set; }
        public int ItemsTo { get; set; }
        public int TotalCountOfItems { get; set; }

        public PagedResult(List<T> items, int totalCountOfItems, int pageSize, int pageNumber)
        {
            Items = items;
            TotalCountOfItems = totalCountOfItems;
            CountOfPages = (int)Math.Ceiling(totalCountOfItems / (double)pageSize);
            ItemsFrom = pageSize * (pageNumber - 1) + 1;
            ItemsTo = ItemsFrom + pageSize - 1;
        }
    }
}
