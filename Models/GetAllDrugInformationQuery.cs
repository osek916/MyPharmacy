using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models
{
    public class GetAllDrugInformationQuery : Pagination
    {
        public char GetByChar { get; set; } = '0';
        public DrugSortBy DrugSortBy { get; set; }
        public SortDirection SortDirection { get; set; } = SortDirection.ASC;
    }
}
