using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models
{
    public class DrugGetAllQuery : Pagination
    {
        public SortDirection SortDirection { get; set; }
        public int NumberPositionsOnPage { get; set; }
        public int ActualPage { get; set; }
        public char GetByChar { get; set; } = '0';
        public DrugSortBy DrugSortBy { get; set; } = DrugSortBy.DrugName;

    }
}
