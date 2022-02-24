using FluentValidation;
using MyPharmacy.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models.Validators
{
    public class UpdateDrugCategoryDtoValidator : AbstractValidator<UpdateDrugCategoryDto>
    {
        public UpdateDrugCategoryDtoValidator(PharmacyDbContext dbContext)
        {

            RuleFor(x => x.CategoryName)
                .MinimumLength(3)
                .MaximumLength(50);
        }
    }
}
