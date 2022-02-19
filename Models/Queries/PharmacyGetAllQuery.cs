using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models
{
    public class PharmacyGetAllQuery : Pagination
    {
        public bool HasPresciptionDrugs { get; set; } = true;
        public char GetByChar { get; set; } = '0';
        public SortDirection SortDirection { get; set; } = SortDirection.ASC;

        public PharmaciesSortBy PharmaciesSortBy { get; set; } 
    }
}
