using FluentValidation;
using MyPharmacy.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models.Validators
{
    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator(PharmacyDbContext dbContext)
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(u => u.Password).MinimumLength(6);

            RuleFor(u => u.ConfirmPassword).Equal(p => p.Password);

            /*
            //unikalność w bazie danych
            RuleFor(u => u.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(x => x.Email == value);
                    if (emailInUse)
                    {
                        context.AddFailure("Email", "That email is taken");
                    }
                });
            */
        }
    }
}
