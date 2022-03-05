using MyPharmacy.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Helpers
{
    public static class PaginationHelper<T, Query> where Query : IPagination
    {
        public static List<T> ReturnPaginatedList(Query query, IQueryable<T> baseQuery)
        {
            return baseQuery
                            .Skip((query.PageNumber - 1) * query.PageSize)
                            .Take(query.PageSize).ToList();
        }
    }
}
