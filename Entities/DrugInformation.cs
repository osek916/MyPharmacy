using System.Collections.Generic;

namespace MyPharmacy.Entities
{
    public class DrugInformation
    {
        public int Id { get; set; }
        public string DrugsName { get; set; }
        public string SubstancesName { get; set; }
        public string Description { get; set; }
        public int NumberOfTablets { get; set; }
        public int MilligramsPerTablets { get; set; }
        public bool LumpSumDrug { get; set; } //lek na ryczałt
        public bool PrescriptionRequired { get; set; }
        public virtual List<Drug> Drugs { get; set; }
        public virtual List<DrugCategory> DrugCategories { get; set; }
    }
}
