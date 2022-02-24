using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MyPharmacy.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models.Validators
{
    public class CreateDrugInformationDtoValidator : AbstractValidator<CreateDrugInformationDto>
    {
        public CreateDrugInformationDtoValidator(PharmacyDbContext dbContext)
        {

            RuleFor(x => x.DrugsName)
                .MinimumLength(2);

            RuleFor(x => x)
                .Custom((value, context) =>
                {
                    var drugAlreadyExist = dbContext.DrugInformations.Any(d => d.DrugsName == value.DrugsName &&
                    d.MilligramsPerTablets == value.MilligramsPerTablets && d.NumberOfTablets == value.NumberOfTablets);

                    if (drugAlreadyExist)
                    {
                        context.AddFailure("That drug information already exist");
                    }
                });

            RuleFor(x => x.MilligramsPerTablets)
                .NotEmpty();

            RuleFor(x => x.NumberOfTablets)
                .NotEmpty();

            RuleFor(x => x.SubstancesName)
                .MinimumLength(2)
                .MaximumLength(50);
        }
    }
}
