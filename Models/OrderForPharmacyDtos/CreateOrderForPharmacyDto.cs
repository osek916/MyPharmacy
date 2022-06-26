using MyPharmacy.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models.OrderForPharmacyDtos
{
    public class CreateOrderForPharmacyDto
    {
        public decimal AdditionalCosts { get; set; }
        public string OrderDescription { get; set; }
        public virtual List<CreateOrderForPharmacyDrugDto> DrugsDtos { get; set; }
        public string StatusName { get; set; }
    }
}
