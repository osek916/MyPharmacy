using FluentValidation;
using MyPharmacy.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models.Validators
{
   
    public class UpdatePharmacyDtoValidator : AbstractValidator<UpdatePharmacyDto>
    {
        public UpdatePharmacyDtoValidator(PharmacyDbContext dbContext)
        {
            

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
