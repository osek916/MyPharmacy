using FluentValidation;
using MyPharmacy.Entities;
using MyPharmacy.Models.UserDtos;
using MyPharmacy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models.Validators
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator(PharmacyDbContext dbContext, IUserContextService userContextService)
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


            RuleFor(u => u.Email)
                .Custom((value, context) =>
                {                 
                    var emailInUse = dbContext.Users.FirstOrDefault(x => x.Email == value);
                    if (emailInUse != null)
                    {
                        if(userContextService.GetUserId != emailInUse.Id)
                        {
                            context.AddFailure("Email", "That email is taken");
                        }        
                    }
                });
        }
    }

}
