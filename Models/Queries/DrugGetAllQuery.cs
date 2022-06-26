using MyPharmacy.Models.Interfaces;

namespace MyPharmacy.Models
{
    public class DrugGetAllQuery : SortParameters, ISortByDirection, ISortByChar, IPagination, IPhrase
    {
        public char GetByChar { get; set; } = '0';
        public DrugSortBy DrugSortBy { get; set; } = DrugSortBy.DrugName;
        public string Phrase { get; set; } = "";
    }
}
