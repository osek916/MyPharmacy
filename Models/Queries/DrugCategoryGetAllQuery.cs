using MyPharmacy.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models
{
    public class DrugCategoryGetAllQuery : ISortByDirection, IPagination, ISortByChar
    {
        public SortDirection SortDirection { get; set; } = SortDirection.ASC;
        public string Phrase { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public char GetByChar { get; set; } = '0';
    }
}
