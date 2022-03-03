using MyPharmacy.Models.Interfaces;

namespace MyPharmacy.Models
{
    public class PharmacyGetAllQuery  : SortParameters, IPagination, ISortByDirection
    {
        public bool HasPresciptionDrugs { get; set; } = true;
        public char GetByChar { get; set; } = '0';
        public PharmaciesSortBy PharmaciesSortBy { get; set; } = PharmaciesSortBy.City;
    }
}
