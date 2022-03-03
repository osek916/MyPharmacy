﻿using System;

namespace MyPharmacy.Models
{
    public class UserRegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public char Gender { get; set; }
        public string Nationality { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int RoleId { get; set; } = 1; //domyślnie jest userem
    }
}
