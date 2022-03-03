namespace MyPharmacy.Models
{
    public class UpdateDrugInformationDto
    {
        public string DrugsName { get; set; }
        public string SubstancesName { get; set; }
        public string Description { get; set; }
        public int NumberOfTablets { get; set; }
        public int MilligramsPerTablets { get; set; }
        public bool LumpSumDrug { get; set; }
        public bool PrescriptionRequired { get; set; }
    }
}
