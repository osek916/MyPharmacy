using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models
{
    
   public class SearchEngineDrugInformationDto : Pagination
   {
        public string DrugsName { get; set; }
        public string SubstancesName { get; set; }
        public bool PrescriptionRequired { get; set; }

   }
    
}
