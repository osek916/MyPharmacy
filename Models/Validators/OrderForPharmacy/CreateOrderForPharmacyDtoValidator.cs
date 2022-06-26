using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MyPharmacy.Entities;
using MyPharmacy.Models.OrderForPharmacyDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Models.Validators.OrderForPharmacy
{
    public class CreateOrderForPharmacyDtoValidator : AbstractValidator<CreateOrderForPharmacyDto>
    {
        public CreateOrderForPharmacyDtoValidator(PharmacyDbContext dbContext)
        {
            var drugInformationExist = dbContext.DrugInformations;
            RuleFor(x => x.StatusName)
                .Custom((value, context) =>
                {
                    var statusAlreadyExist = dbContext.Statuses.Any(s => s.Name == value);
                    if (!statusAlreadyExist)
                        context.AddFailure("This option of status doesn't exist in the database");
                });

            RuleFor(x => x.DrugsDtos)
                .Custom((value, context) =>
                {
                    //var drugFromDbContext = dbContext.Drugs.Include(x => x.DrugInformation).Select(drugDto => new CreateOrderForPharmacyDrugDto() { });
                    //var drugInformationExist = dbContext.DrugInformations;
                    foreach (var drug in value)
                    {
                        if (!drugInformationExist.Any(d => d.MilligramsPerTablets == drug.MilligramsPerTablets && d.NumberOfTablets == drug.NumberOfTablets &&
                        d.SubstancesName == drug.SubstancesName && d.DrugsName == drug.DrugsName))
                        {
                            context.AddFailure(string.Format("{0} {1} {2} {3} doesn't exist in list of drugs. Please check this position",
                                drug.DrugsName, drug.SubstancesName, drug.NumberOfTablets, drug.MilligramsPerTablets));
                        }
                    }
                });

            RuleForEach(x => x.DrugsDtos)
                .Custom((value, context) =>
                {
                    if (value.Price < 0)
                        context.AddFailure(string.Format("{0} {1} {2} {3} can't have a price lower than zero",
                                value.DrugsName, value.SubstancesName, value.NumberOfTablets, value.MilligramsPerTablets, value.Price));
                });
        }
    }
}
