using MyPharmacy.Models.Interfaces;

namespace MyPharmacy.Models
{
    public class DrugCategoryGetAllQuery : SortParameters, ISortByDirection, IPagination, ISortByChar
    {
        public char GetByChar { get; set; } = '0';
    }
}
