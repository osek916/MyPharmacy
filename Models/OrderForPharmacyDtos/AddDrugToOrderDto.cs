using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models.OrderForPharmacyDtos
{
    public class AddDrugToOrderDto
    {
        public string DrugsName { get; set; }
        public string SubstancesName { get; set; }
        public int NumberOfTablets { get; set; }
        public int MilligramsPerTablets { get; set; }
    }
}
