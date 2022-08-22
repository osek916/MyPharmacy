using Microsoft.AspNetCore.Authorization;

namespace MyPharmacy.Authorization
{
    public class HasAPharmacyRequirement : IAuthorizationRequirement
    {
        public bool HasAPharmacy { get; set; }
        public HasAPharmacyRequirement(bool hasAPharmacy)
        {
            HasAPharmacy = hasAPharmacy;
        }
    }
}
