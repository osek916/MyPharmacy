using MyPharmacy.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models
{
    public class SearchEngineDrugInformationQuery : IPagination, ISortByDirection, ISortByChar
    {
        public bool PrescriptionRequired { get; set; } = true;
        public DrugSortBy DrugSortBy { get; set; } = DrugSortBy.DrugName;
        public SortDirection SortDirection { get; set; } = SortDirection.ASC;
        public string Phrase { get; set; } = "";
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public char GetByChar { get; set; } = '0';
    }
}
