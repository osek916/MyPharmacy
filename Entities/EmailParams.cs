namespace MyPharmacy.Entities
{
    public class EmailParams
    {
        public int Id { get; set; }
        public string HostSmpt { get; set; } //= "smtp.gmail.com";
        public bool EnableSsl { get; set; } //= true;
        public int Port { get; set; } //= 587;
        public string SenderEmail { get; set; }
        public string SenderEmailPassword { get; set; }
        public string SenderName { get; set; }
        public int PharmacyId { get; set; }
        public virtual Pharmacy Pharmacy { get; set; }
    }
}
