using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Entities
{
    public class OrderByClient
    {
        public int Id { get; set; }       
        public DateTime DateOfOrder { get; set; }
        public DateTime? DateOfReceipt { get; set; }
        public bool IsPersonalPickup { get; set; } //jeżeli true to ustawia adres z apteki
        public decimal Price { get; set; }

        public int? StatusId { get; set; }
        public virtual Status Status { get; set; }
        public int? UserId { get; set; }
        public virtual User User { get; set; }
        public int? AddressId { get; set; }
        public virtual Address Address { get; set; } //jeżeli jest inny adres niż apteka to jest dostarczenie i zabrania się na receptę
        public virtual ICollection<Drug> Drugs { get; set; }
    }
}
