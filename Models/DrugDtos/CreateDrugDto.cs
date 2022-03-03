namespace MyPharmacy.Models
{
    public class CreateDrugDto
    {
        public string DrugsName { get; set; }
        public string SubstancesName { get; set; }
        public int NumberOfTablets { get; set; }
        public int MilligramsPerTablets { get; set; }
        public int AmountOfPackages { get; set; }
        public decimal Price { get; set; }

    }
}
