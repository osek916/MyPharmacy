﻿using MyPharmacy.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models.UserDtos
{
    public class UpdateUserDtoWithRole : UpdateUserDto, IUpdateUserDto, IUpdateUserDtoWithRole
    {
        public int RoleId { get; set; }
        public int? PharmacyId { get; set; }
    }
}
