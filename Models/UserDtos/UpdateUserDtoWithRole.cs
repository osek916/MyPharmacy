using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models.UserDtos
{
    public class UpdateUserDtoWithRole : UpdateUserDto
    {
        public int RoleId { get; set; }
    }
}
