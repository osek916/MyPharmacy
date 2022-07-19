using FluentValidation;
using MyPharmacy.Models.Queries;
using System.Linq;

namespace MyPharmacy.Models.Validators.OrderForPharmacy
{
    public class OrderForPharmacyGetAllQueryValidator : AbstractValidator<OrderForPharmacyGetAllQuery>
    {
        private string[] sortByColumnAllowedVariables = {nameof(Entities.OrderForPharmacy.DateOfOrder),
        nameof(Entities.OrderForPharmacy.DateOfReceipt),
        nameof(Entities.OrderForPharmacy.Status.Name),
        nameof(Entities.OrderForPharmacy.Price),
        nameof(Entities.OrderForPharmacy.UserId)};

        private int[] pageSizeAllowedVariables = new[] { 5, 10, 15, 20 };

        public OrderForPharmacyGetAllQueryValidator()
        {
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
