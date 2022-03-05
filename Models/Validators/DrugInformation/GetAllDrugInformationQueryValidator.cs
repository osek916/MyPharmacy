﻿using FluentValidation;
using MyPharmacy.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models.Validators.DrugInformation
{
    public class GetAllDrugInformationQueryValidator : AbstractValidator<GetAllDrugInformationQuery>
    {
        private string[] sortByColumnAllowedVariables = { nameof(Entities.DrugInformation.DrugsName), nameof(Entities.DrugInformation.SubstancesName) };
        private int[] pageSizeAllowedVariables = new[] { 5, 10, 15, 20 };

        public GetAllDrugInformationQueryValidator()
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
