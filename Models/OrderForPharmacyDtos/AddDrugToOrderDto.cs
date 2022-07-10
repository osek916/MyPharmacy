using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models.OrderForPharmacyDtos
{
    public class AddDrugToOrderDto
    {
        [Required]        
        public string DrugsName { get; set; }
        [Required]
        public string SubstancesName { get; set; }
        [Required]
        public int NumberOfTablets { get; set; }
        [Required]
        public int MilligramsPerTablets { get; set; }
        public int AmountOfPackages { get; set; } = 0;
        public decimal AdditionalCosts { get; set; } = 0;
        public decimal Price { get; set; } = 0;
    }
}
