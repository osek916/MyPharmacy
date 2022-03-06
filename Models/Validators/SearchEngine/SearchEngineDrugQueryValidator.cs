using FluentValidation;
using MyPharmacy.Entities;
using System;
using System.Linq;

namespace MyPharmacy.Models.Validators.SearchEngine
{
    public class SearchEngineDrugQueryValidator : AbstractValidator<SearchEngineDrugQuery>
    {
        private string[] sortByColumnAllowedVariables = { nameof(Entities.Pharmacy.Address.City), nameof(Entities.Pharmacy.Name) };
        private int[] pageSizeAllowedVariables = new[] { 5, 10, 15, 20 };

        public SearchEngineDrugQueryValidator()
        {
            RuleFor(d => d.Phrase).NotEmpty();
            RuleFor(d => d.SortBy).Must(value => sortByColumnAllowedVariables.Contains(value))
                .WithMessage($"SortBy must be in {string.Join(", ", sortByColumnAllowedVariables)}");


            RuleFor(d => d.PageSize).Custom((value, context) =>
            {
                if (!pageSizeAllowedVariables.Contains(value))
                    context.AddFailure("PageSize", $"PageSize must be in [{string.Join(", ", pageSizeAllowedVariables)}]");
            });

            RuleFor(d => d.PageNumber).GreaterThanOrEqualTo(1);
        }
    }
    
}
