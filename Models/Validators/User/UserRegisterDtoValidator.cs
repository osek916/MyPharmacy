using FluentValidation;
using MyPharmacy.Entities;
using System.Linq;

namespace MyPharmacy.Models.Validators
{
    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        string specialChar = @"|!#$%&/()=?»«@£§€{}.-;~`'<>_,";
        public UserRegisterDtoValidator(PharmacyDbContext dbContext)
        {
            RuleFor(u => u.FirstName)
                .MinimumLength(2)
                .MaximumLength(25);

            RuleFor(u => u.LastName)
                .MinimumLength(2)
                .MaximumLength(25);

            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(u => u.Gender)
                .NotEmpty();

            RuleFor(u => u.Nationality)
                .MinimumLength(2)
                .MaximumLength(25);

            RuleFor(u => u.Password)
                .Custom((value, context) =>
                {
                    bool isNumber = false;
                    bool hasNumber = false;
                    for (int i = 0; i < value.Length; i++)
                    {
                        hasNumber = int.TryParse(value[i].ToString(), out int result);

                        if (hasNumber)
                            isNumber = true;
                    }

                    if (!isNumber)
                        context.AddFailure("Password", "That password don't has any number");

                }).Custom((value, context) =>
                {
                    bool hasSpecialChar = false;
                    for (int i = 0; i < value.Length; i++)
                    {
                        if(specialChar.Contains(value[i]))
                        {
                            hasSpecialChar = true;
                        }
                    }
                    if (!hasSpecialChar)
                        context.AddFailure("Password", "That password don't has any special char");
                })
                .MinimumLength(6).MaximumLength(30);
            

            RuleFor(u => u.ConfirmPassword).Equal(p => p.Password);

            
            RuleFor(u => u.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(x => x.Email == value);
                    if (emailInUse)
                    {
                        context.AddFailure("Email", "That email is taken");
                    }
                });
            
        }
    }
}
