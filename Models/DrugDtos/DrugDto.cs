namespace MyPharmacy.Models
{
    public class DrugDto
    {
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
