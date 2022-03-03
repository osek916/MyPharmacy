using FluentValidation;
using MyPharmacy.Entities;

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
