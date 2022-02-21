using FluentValidation;
using MyPharmacy.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models.Validators
{
    public class UpdateDrugDtoValidator : AbstractValidator<UpdateDrugDto>
    {
        public UpdateDrugDtoValidator(PharmacyDbContext dbContext)
        {
 
            RuleFor(x => x.AmountOfPackages)
                .Custom((value, context) =>
                {
                    if (value < 1)
                    {
                        context.AddFailure("AmountOfPackages", "The number of packages must not be less than 0");
                    }
                });

            RuleFor(x => x.Price)
                .NotEmpty();

        }
    }
}
