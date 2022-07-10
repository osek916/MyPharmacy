using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models.OrderForPharmacyDtos
{
    public class UpdateOrderForPharmacyDto
    {
        public DateTime DateOfOrder { get; set; }
        public decimal Price { get; set; }
        public string OrderDescription { get; set; }
        public virtual List<CreateOrderForPharmacyDrugDto> DrugsDtos { get; set; }

    }
}
