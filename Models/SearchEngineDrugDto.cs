using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models
{
    public class SearchEngineDrugDto
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string DrugsName { get; set; }
        public string SubstancesName { get; set; }
        public int NumberOfTablets { get; set; }
        public int MilligramsPerTablets { get; set; }
        public bool LumpSumDrug { get; set; } //lek na ryczałt
        public bool PrescriptionRequired { get; set; }
        public int AmountOfPackages { get; set; }
        public decimal Price { get; set; }
    }
}
