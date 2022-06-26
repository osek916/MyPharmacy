using MyPharmacy.Models.Interfaces;

namespace MyPharmacy.Models
{
    public abstract class SortParameters : ISortByDirection, IPagination
    {
        public SortDirection SortDirection { get; set; } = SortDirection.ASC;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}
