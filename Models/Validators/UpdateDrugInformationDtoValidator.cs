using FluentValidation;
using MyPharmacy.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models.Validators
{
  
    public class UpdateDrugInformationDtoValidator : AbstractValidator<UpdateDrugInformationDto>
    {
        public UpdateDrugInformationDtoValidator(PharmacyDbContext dbContext)
        {


            RuleFor(x => x.DrugsName)
                .MinimumLength(2);
       

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
