using MyPharmacy.Entities;
using MyPharmacy.Models.Interfaces;

namespace MyPharmacy.Models.Queries
{
    public class OrderByClientGetAllQuery : SortParameters, ISortByDirection, IPagination
    {
        public string SortBy { get; set; } = nameof(OrderByClient.DateOfOrder);
    }
}
