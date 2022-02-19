using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models
{
    public class DrugCategoryGetAllQuery : Pagination
    {
        public SortDirection SortDirection { get; set; } = SortDirection.ASC;

    }
}
