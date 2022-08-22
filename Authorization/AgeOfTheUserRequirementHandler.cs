using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace MyPharmacy.Authorization
{
    public class AgeOfTheUserRequirementHandler : AuthorizationHandler<AgeOfTheUserRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AgeOfTheUserRequirement requirement)
        {
            var dateOfBirth = DateTime.Parse(context.User.FindFirst(c => c.Type == "DateOfBirth").Value);

            if(dateOfBirth.AddYears(requirement.Age) > DateTime.Today)
                context.Succeed(requirement);
            
            return Task.CompletedTask;
        }
    }
}
