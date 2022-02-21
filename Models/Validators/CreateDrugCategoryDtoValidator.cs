using FluentValidation;
using MyPharmacy.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models.Validators
{
    public class CreateDrugCategoryDtoValidator : AbstractValidator<CreateDrugCategoryDto>
    {
        public CreateDrugCategoryDtoValidator(PharmacyDbContext dbContext)
        {

            RuleFor(x => x.CategoryName)
                .MinimumLength(3)
                .MaximumLength(50);

            

            RuleFor(x => x)
                .Custom((value, context) =>
                {
                    var drugAlreadyExist = dbContext.DrugCategories.Any(d => d.CategoryName == value.CategoryName);

                    if (drugAlreadyExist)
                    {
                        context.AddFailure("That drug category already exist");
                    }
                });
        }
    }
}
