using FluentValidation;
using MyPharmacy.Entities;

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
                })
                .NotEmpty();

            RuleFor(x => x.Price)
                .Custom((value, context) =>
                {
                    if(value < 0)
                    {
                        context.AddFailure("Price", "The price must not be less than 0");
                    }
                })
                .NotEmpty();

        }
    }
}
