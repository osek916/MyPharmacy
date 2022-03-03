namespace MyPharmacy.Models.Interfaces
{
    public interface IUpdateUserDtoWithRole
    {
        public int RoleId { get; set; }
        public int? PharmacyId { get; set; }
    }
}
