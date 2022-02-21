using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models.Interfaces
{
    public interface IPagination
    {
        public int PageNumber { get; set; } 
        public int PageSize { get; set; } 
        public int NumberPositionsOnPage { get; set; }
        public int ActualPage { get; set; }
    }
}
