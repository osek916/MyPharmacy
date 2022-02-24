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
    public class UpdateUserRoleAndPharmacyIdValidator : AbstractValidator<UpdateUserRoleAndPharmacyId>
    {
        private int[] allowedRoleId = new[] { 1, 2, 3, 4 };

        public UpdateUserRoleAndPharmacyIdValidator(PharmacyDbContext dbContext, IUserContextService userContextService)
        {
            RuleFor(x => x.RoleId).Custom((value, context) =>
            {
                if (!allowedRoleId.Contains(value))
                {
                    context.AddFailure("RoleId", $"RoleId must be one of these values: {string.Join(", ", allowedRoleId)}");
                }

                if (userContextService.Role != "Admin" && (value == 3 || value == 4))
                {
                    context.AddFailure("RoleId", "Your account doesn't have sufficient permissions");
                }
            });
        }
    }
}
