using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MyPharmacy.Entities;
using MyPharmacy.Services;
using System.Linq;

namespace MyPharmacy.Models.Validators
{
    public class CreateDrugDtoValidator : AbstractValidator<CreateDrugDto>
    {
        private readonly IUserContextService _userContextService;
        public CreateDrugDtoValidator(PharmacyDbContext dbContext, IUserContextService userContextService)
        {
            _userContextService = userContextService;

            RuleFor(x => x.AmountOfPackages)
                .Custom((value, context) =>
                {
                    if(value < 1)
                    {
                        context.AddFailure("AmountOfPackages", "The number of packages must not be less than 0");
                    }
                });

            RuleFor(x => x)
                .Custom((value, context) =>
                {
                    var drugAlreadyExist = dbContext.Drugs.Include(d => d.DrugInformation).Any(d => d.DrugInformation.DrugsName == value.DrugsName &&
                    d.DrugInformation.MilligramsPerTablets == value.MilligramsPerTablets && d.DrugInformation.NumberOfTablets == value.NumberOfTablets && d.PharmacyId == _userContextService.PharmacyId);
                    
                    if(drugAlreadyExist)
                    {
                        context.AddFailure("That drug already exist");
                    }
                });

            RuleFor(x => x.MilligramsPerTablets)
                .NotEmpty();

            RuleFor(x => x.NumberOfTablets)
                .NotEmpty();

            RuleFor(x => x.Price)
                .NotEmpty();

            RuleFor(x => x.SubstancesName)
                .MinimumLength(2)
                .MaximumLength(50);


        }
    }
}
