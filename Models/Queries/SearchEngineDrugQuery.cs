using MyPharmacy.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models
{
    public class SearchEngineDrugQuery : ISortByDirection, IPagination
    {
        public string City { get; set; } 
        public SortDirection SortDirection { get; set; } = SortDirection.ASC;
        public PharmaciesSortBy PharmaciesSortBy { get; set; } = PharmaciesSortBy.Name;
        public string Phrase { get; set; } = "";
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}
