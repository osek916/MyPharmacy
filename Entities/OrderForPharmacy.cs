using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Entities
{
    public class OrderForPharmacy
    {
        public int Id { get; set; }
        public DateTime DateOfOrder { get; set; }
        public DateTime? DateOfReceipt { get; set; }
        public decimal Price { get; set; }
        public string OrderDescription { get; set; }
        public int? StatusId { get; set; }
        public virtual Status Status { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int PharmacyId { get; set; }
        public virtual Pharmacy Pharmacy { get; set; }
        public virtual List<Drug> Drugs { get; set; }

    }
}
