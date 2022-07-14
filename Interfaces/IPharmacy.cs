using MyPharmacy.Entities;
using System.Collections.Generic;

namespace MyPharmacy.Interfaces
{
    public interface IPharmacy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasPresciptionDrugs { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }
        public int? CreatedByUserId { get; set; }

        public List<Drug> Drugs { get; set; }

        public int AddressId { get; set; }
        public  Address Address { get; set; }
        public  List<User> Users { get; set; }
        public  EmailParams EmailParams { get; set; }
    }
}
