using Microsoft.AspNetCore.Authorization;

namespace MyPharmacy.Authorization
{
    public class AgeOfTheUserRequirement : IAuthorizationRequirement
    {
        public int Age { get; set; }
        public AgeOfTheUserRequirement(int age)
        {
            Age = age;
        }
    }
}
