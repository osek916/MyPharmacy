using FluentValidation;
using MyPharmacy.Entities;
using MyPharmacy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models.Validators
{
   
    public class UpdatePharmacyDtoValidator : AbstractValidator<UpdatePharmacyDto>
    {
        public UpdatePharmacyDtoValidator(PharmacyDbContext dbContext, IUserContextService userContextService)
        {
            RuleFor(x => x.ContactEmail)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Pharmacies.FirstOrDefault(p => p.ContactEmail == value);
                    if (emailInUse != null)
                    {
                        if (userContextService.GetUserId != emailInUse.Id)
                        {
                            context.AddFailure("ContactEmail", "That ContactEmail is taken");
                        }
                        
                    }
                })
                .EmailAddress();

            RuleFor(x => x.ContactNumber)
                .NotEmpty();

            RuleFor(x => x.City)
                .MinimumLength(2);

            RuleFor(x => x.HasPresciptionDrugs)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.PostalCode)
                .NotEmpty();

            RuleFor(x => x.Street)
                .NotEmpty();
        }
    }
}
