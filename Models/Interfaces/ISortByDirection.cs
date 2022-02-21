using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models.Interfaces
{
    public interface ISortByDirection
    {
        public SortDirection SortDirection { get; set; }
    }
}
