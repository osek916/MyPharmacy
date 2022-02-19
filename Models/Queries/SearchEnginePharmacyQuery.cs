using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models
{
    public class SearchEnginePharmacyQuery : Pagination
    {
        public bool HasPresciptionDrugs { get; set; } = true;
        
        public PharmaciesSortBy PharmaciesSortBy { get; set; }
        
    }
}
