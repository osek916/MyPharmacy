using MyPharmacy.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models
{
    public class PharmacyGetAllQuery  : ISortByDirection, ISortByChar, IPagination
    {
        public bool HasPresciptionDrugs { get; set; } = true;
        public char GetByChar { get; set; } = '0';
        public SortDirection SortDirection { get; set; } = SortDirection.ASC;

        public PharmaciesSortBy PharmaciesSortBy { get; set; } = PharmaciesSortBy.City;
        public string Phrase { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public int NumberPositionsOnPage { get; set; }
        public int ActualPage { get; set; }
        //public SortDirection SortDirection { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
