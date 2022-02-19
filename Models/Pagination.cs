using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models
{
    public abstract class Pagination
    {
        public string Phrase { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public int NumberPositionsOnPage { get; set; }
        public int ActualPage { get; set; }
    }
}
