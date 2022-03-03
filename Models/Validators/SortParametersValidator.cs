using FluentValidation;
using System;
using System.Linq;

namespace MyPharmacy.Models.Validators
{
    public class SortParametersValidator : AbstractValidator<SortParameters>
    {
        private int[] pageSizeAllowedVariables = new[] { 5, 10, 15, 20 };

        public SortParametersValidator()
        {
            RuleFor(d => d.PageSize).Custom((value, context) =>
            {
                if (!pageSizeAllowedVariables.Contains(value))
                    context.AddFailure("PageSize", $"PageSize must be in [{string.Join(", ", pageSizeAllowedVariables)}]");
            });

            RuleFor(d => d.PageNumber).GreaterThanOrEqualTo(1);
        }
    }
}
