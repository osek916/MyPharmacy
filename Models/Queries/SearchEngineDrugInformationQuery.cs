using MyPharmacy.Entities;
using MyPharmacy.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models
{
    public class SearchEngineDrugInformationQuery : SortParameters, IPagination, ISortByDirection, ISortBy
    {
        public string SortBy { get; set; } = nameof(DrugInformation.DrugsName);
    }
}

/*
 * public class SearchEngineDrugInformationQuery : IPagination, ISortByDirection, ISortBy
    {
        public string SortBy { get; set; } = nameof(DrugInformation.DrugsName);
        public SortDirection SortDirection { get; set; } = SortDirection.ASC;
        public string Phrase { get; set; } = "";
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
*/