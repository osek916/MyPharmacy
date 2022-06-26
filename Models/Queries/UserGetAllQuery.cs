using MyPharmacy.Models.Enums;
using MyPharmacy.Models.Interfaces;

namespace MyPharmacy.Models.Queries
{
    public class UserGetAllQuery : SortParameters, IPagination, ISortByChar, ISortByDirection, IPhrase
    {
        public UserSortBy UserSortBy { get; set; } = UserSortBy.City;
        public char GetByChar { get; set; } = '0';
        public string Phrase { get; set; } = "";
    }
}
