using MyPharmacy.Entities;
using MyPharmacy.Models.Interfaces;

namespace MyPharmacy.Models
{
    public class SearchEngineDrugQuery : SortParameters, IPagination, ISortByDirection, ISortBy, IPhrase
    {
        public string City { get; set; }
        public string SortBy { get; set; } = nameof(Pharmacy.Name);
        public string Phrase { get; set; } = "";
    }
}
