using System.Collections.Generic;

namespace MyPharmacy.Models.OrderByClientDtos
{
    public class CreateOrderByClientDto
    {
        public bool IsPersonalPickup { get; set; }       
        public virtual List<CreateOrderByClientDrugDto> DrugDtos { get; set; }
        public int? PharmacyId { get; set; }
    }
}
