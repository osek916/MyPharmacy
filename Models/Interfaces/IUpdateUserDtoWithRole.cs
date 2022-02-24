using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models.Interfaces
{
    public interface IUpdateUserDtoWithRole
    {
        public int RoleId { get; set; }
        public int? PharmacyId { get; set; }
    }
}
