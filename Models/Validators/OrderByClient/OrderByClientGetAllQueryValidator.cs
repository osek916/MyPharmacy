using FluentValidation;
using MyPharmacy.Models.Queries;
using System.Linq;

namespace MyPharmacy.Models.Validators.OrderByClient
{
    public class OrderByClientGetAllQueryValidator : AbstractValidator<OrderByClientGetAllQuery>
    {
        private string[] sortByColumnAllowedVariables = { nameof( Entities.OrderByClient.DateOfOrder),
        nameof(Entities.OrderByClient.Status.Name ),
        nameof(Entities.OrderByClient.DateOfReceipt),
        nameof(Entities.OrderByClient.Price)};

        private int[] pageSizeAllowedVariables = new[] { 5, 10, 15, 20 };

        public OrderByClientGetAllQueryValidator()
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
