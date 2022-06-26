using MyPharmacy.Models.Interfaces;

namespace MyPharmacy.Models
{
    public class DrugCategoryGetAllQuery : SortParameters, ISortByDirection, IPagination, ISortByChar, IPhrase
    {
        public char GetByChar { get; set; } = '0';
        public string Phrase { get; set; } = "";
    }
}
