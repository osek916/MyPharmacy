using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyPharmacy.Authorization
{
    public class HasAPharmacyRequirementHandler : AuthorizationHandler<HasAPharmacyRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasAPharmacyRequirement requirement)
        {
            var pharmacyId = int.Parse(context.User.FindFirst(c => c.Type == "PharmacyId").Value);

            if(pharmacyId != 0)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
