using MyPharmacy.Entities;
using MyPharmacy.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models
{
    public class SearchEngineDrugQuery : SortParameters, IPagination, ISortByDirection, ISortBy
    {
        public string City { get; set; }
        public string SortBy { get; set; } = nameof(Pharmacy.Name);
    }
}
