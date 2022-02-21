using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models
{
    public class UpdateDrugInformationDto
    {
        [Required]
        public string DrugsName { get; set; }
        [Required]
        public string SubstancesName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int NumberOfTablets { get; set; }
        [Required]
        public int MilligramsPerTablets { get; set; }
        [Required]
        public bool LumpSumDrug { get; set; }
        [Required]
        public bool PrescriptionRequired { get; set; }
        public List<string> DrugCategoryNames { get; set; }
    }
}
