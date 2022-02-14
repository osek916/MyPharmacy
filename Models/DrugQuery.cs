using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models
{
    public class DrugQuery
    {
        public string Phrase { get; set; }
        public string SortCategory { get; set; }
        public SortDirection SortDirection { get; set; }
        public int NumberPositionsOnPage { get; set; }
        public int ActualPage { get; set; }
        public string City { get; set; }
    }
}
