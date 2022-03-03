using MyPharmacy.Models.Interfaces;
using System;

namespace MyPharmacy.Models.UserDtos
{
    public class UpdateUserDto : IUpdateUserDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public char Gender { get; set; }
    }
}
