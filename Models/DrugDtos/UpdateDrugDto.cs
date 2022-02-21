using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models
{
    public class UpdateDrugDto
    {
        public int AmountOfPackages { get; set; }
        public decimal Price { get; set; }
        public int OptionalId { get; set; } = 0;
    }
}
