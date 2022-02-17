using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models
{
    public class DrugDto
    {
        
        public int AmountOfPackages { get; set; }
        public decimal Price { get; set; }
        public bool LumpSumDrug { get; set; } //lek na ryczałt
        public bool PrescriptionRequired { get; set; }
        public int PharmacyId { get; set; }
    }
}
