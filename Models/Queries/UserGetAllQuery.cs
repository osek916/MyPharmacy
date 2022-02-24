using MyPharmacy.Models.Enums;
using MyPharmacy.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models.Queries
{
    public class UserGetAllQuery : IPagination, ISortByChar, ISortByDirection
    {
        public UserSortBy UserSortBy { get; set; } = UserSortBy.City;
        public SortDirection SortDirection { get; set; } = SortDirection.ASC;
        public char GetByChar { get; set; } = '0';
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}
