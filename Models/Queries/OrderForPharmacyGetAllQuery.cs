using MyPharmacy.Entities;
using MyPharmacy.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models.Queries
{
    public class OrderForPharmacyGetAllQuery : SortParameters, ISortByDirection, IPagination
    {
        public string SortBy { get; set; } = nameof(OrderForPharmacy.DateOfOrder);
        public int? year { get; set; }
    }
}
