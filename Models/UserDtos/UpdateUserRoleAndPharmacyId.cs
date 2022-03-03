using MyPharmacy.Models.Interfaces;

namespace MyPharmacy.Models.UserDtos
{
    public class UpdateUserRoleAndPharmacyId : IUpdateUserDtoWithRole
    {
        public int RoleId { get; set; }
        public int? PharmacyId { get; set; }
    }
}
