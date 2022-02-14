using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Entities
{
    public class Drug
    {
        public int Id { get; set; }
        public int AmountOfPackages { get; set; }
        public decimal Price { get; set; }

        public int? DrugInformationId { get; set; }
        public virtual DrugInformation DrugInformation { get; set; }
        public int PharmacyId { get; set; }
        public virtual Pharmacy Pharmacy { get; set; }
        public List<OrderByClient> OrderByClients { get; set; }

    }
}
