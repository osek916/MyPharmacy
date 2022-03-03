namespace MyPharmacy.Models
{

    public class UpdatePharmacyDto
    {
        public string Name { get; set; }
        public bool HasPresciptionDrugs { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public int OptionalPharmacyId { get; set; } = 0;
    }
}
