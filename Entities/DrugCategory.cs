using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Entities
{
    public class DrugCategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public virtual List<DrugInformation> DrugInformations { get; set; }
    }
}
