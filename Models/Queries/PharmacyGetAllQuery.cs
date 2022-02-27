using MyPharmacy.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models
{
    public class PharmacyGetAllQuery  : SortParameters, IPagination, ISortByDirection
    {
        public bool HasPresciptionDrugs { get; set; } = true;
        public char GetByChar { get; set; } = '0';
        public PharmaciesSortBy PharmaciesSortBy { get; set; } = PharmaciesSortBy.City;
    }
}
