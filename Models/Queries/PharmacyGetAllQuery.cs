using MyPharmacy.Entities;
using MyPharmacy.Models.Interfaces;

namespace MyPharmacy.Models
{
    public class PharmacyGetAllQuery  : SortParameters, IPagination, ISortByDirection
    {
        public bool HasPresciptionDrugs { get; set; } = true;
        public string SortBy { get; set; } = nameof(Pharmacy.Address.City);
    }
}
