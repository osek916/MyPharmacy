using FluentValidation;
using MyPharmacy.Entities;
using System.Linq;

namespace MyPharmacy.Models.Validators
{
    public class CreatePharmacyDtoValidator : AbstractValidator<CreatePharmacyDto>
    {
        public CreatePharmacyDtoValidator(PharmacyDbContext dbContext)
        {
            RuleFor(x => x.ContactEmail)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Pharmacies.Any(p => p.ContactEmail == value);
                    if (emailInUse)
                    {
                        context.AddFailure("ContactEmail", "That ContactEmail is taken");
                    }
                })
                .EmailAddress();

            RuleFor(x => x.ContactNumber)
                .MinimumLength(9);

            RuleFor(x => x.City)
                .MinimumLength(2);

            RuleFor(x => x.HasPresciptionDrugs)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.PostalCode)
                .MinimumLength(5);
        }
    }
}
