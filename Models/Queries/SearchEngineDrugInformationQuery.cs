using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models
{
    public class SearchEngineDrugInformationQuery : Pagination
    {
        public bool PrescriptionRequired { get; set; } = true;
        public DrugSortBy DrugSortBy { get; set; }
    }
}
