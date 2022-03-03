namespace MyPharmacy.Models
{
    public class SearchEnginePharmacyDto
    {
        public string Name { get; set; }
        public bool HasPresciptionDrugs { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
    }
}
