using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models
{
    public class CreatePharmacyDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public bool HasPresciptionDrugs { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        [MaxLength(50)]
        public string Street { get; set; }
        public string PostalCode { get; set; }
    }
}
