using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models
{
    public class CreateDrugDto
    {
        [Required]
        public string DrugsName { get; set; }
        [Required]
        public string SubstancesName { get; set; }
        [Required]
        public int NumberOfTablets { get; set; }
        [Required]
        public int MilligramsPerTablets { get; set; }
        [Required]
        public int AmountOfPackages { get; set; }
        [Required]
        public decimal Price { get; set; }

    }
}
