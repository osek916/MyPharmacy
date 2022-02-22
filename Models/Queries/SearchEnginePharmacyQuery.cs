using MyPharmacy.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models
{
    public class SearchEnginePharmacyQuery : ISortByDirection, ISortByChar, IPagination
    {
        public bool HasPresciptionDrugs { get; set; } = true;

        public PharmaciesSortBy PharmaciesSortBy { get; set; } = PharmaciesSortBy.Name;

        public SortDirection SortDirection { get; set; } = SortDirection.ASC;
        public char GetByChar { get; set; } = '0';
        public string Phrase { get; set; } = "";
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;

    }
}
