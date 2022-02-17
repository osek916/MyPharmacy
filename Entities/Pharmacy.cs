using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Entities
{
    public class Pharmacy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasPresciptionDrugs { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }
        public int? CreatedByUserId { get; set; } //nowo dodane
        //leki
        public virtual List<Drug> Drugs { get; set; }
        //adres
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
    }
}
