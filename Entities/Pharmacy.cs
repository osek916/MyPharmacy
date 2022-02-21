using MyPharmacy.Models.Interfaces;
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
        public int? CreatedByUserId { get; set; } 
        
        public virtual List<Drug> Drugs { get; set; }
        
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
