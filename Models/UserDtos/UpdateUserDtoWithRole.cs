using MyPharmacy.Models.Interfaces;

namespace MyPharmacy.Models.UserDtos
{
    public class UpdateUserDtoWithRole : UpdateUserDto, IUpdateUserDto, IUpdateUserDtoWithRole
    {
        public int RoleId { get; set; }
        public int? PharmacyId { get; set; }
    }
}
