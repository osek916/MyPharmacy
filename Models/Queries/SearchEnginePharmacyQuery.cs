using MyPharmacy.Entities;
using MyPharmacy.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models
{
    public class SearchEnginePharmacyQuery : SortParameters, ISortByDirection, IPagination, ISortBy
    {
        public bool HasPresciptionDrugs { get; set; }
        public string SortBy { get; set; } = nameof(Pharmacy.Address.City);
    }
}
