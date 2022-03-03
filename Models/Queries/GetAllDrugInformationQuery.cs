using MyPharmacy.Models.Interfaces;

namespace MyPharmacy.Models
{
    public class GetAllDrugInformationQuery : ISortByDirection, ISortByChar, IPagination
    {
        public char GetByChar { get; set; } = '0';
        public DrugSortBy DrugSortBy { get; set; } = DrugSortBy.DrugName;
        public SortDirection SortDirection { get; set; } = SortDirection.ASC;
        public string Phrase { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}
