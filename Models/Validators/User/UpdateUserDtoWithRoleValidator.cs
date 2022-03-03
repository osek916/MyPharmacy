using FluentValidation;
using MyPharmacy.Entities;
using MyPharmacy.Models.UserDtos;
using MyPharmacy.Services;
using System;
using System.Linq;

namespace MyPharmacy.Models.Validators
{


    public class UpdateUserDtoWithRoleValidator : AbstractValidator<UpdateUserDtoWithRole>
    {

        private int[] allowedRoleId = new[] { 1, 2, 3, 4 };
        
        public UpdateUserDtoWithRoleValidator(PharmacyDbContext dbContext, IUserContextService userContextService) 
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
                        if (userContextService.GetUserId != emailInUse.Id)
                        {
                            context.AddFailure("Email", "That email is taken");
                        }
                    }
                });

            RuleFor(x => x.RoleId).Custom((value, context) =>
            {
                if(!allowedRoleId.Contains(value))
                {
                    context.AddFailure("RoleId", $"RoleId must be one of these values: {string.Join(", ", allowedRoleId)}");
                }

                if(userContextService.Role != "Admin" && (value == 3 || value == 4))
                {
                    context.AddFailure("RoleId", "Your account doesn't have sufficient permissions");
                }
            });
        }
    }
}
