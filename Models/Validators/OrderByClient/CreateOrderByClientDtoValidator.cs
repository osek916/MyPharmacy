using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MyPharmacy.Entities;
using MyPharmacy.Models.OrderByClientDtos;
using System.Linq;

namespace MyPharmacy.Models.Validators.OrderByClient
{
    public class CreateOrderByClientDtoValidator : AbstractValidator<CreateOrderByClientDto>
    {
        public CreateOrderByClientDtoValidator(PharmacyDbContext dbContext)
        {
            var drugInformationExist = dbContext.DrugInformations.AsNoTracking();
            
            RuleFor(o => o.IsPersonalPickup)
                .NotNull()
                .NotEmpty();


            RuleForEach(o => o.DrugDtos)
                .Custom((drugInformation, context) =>
                {
                    var drugInformationFromDatabase = dbContext.DrugInformations
                    .FirstOrDefault((d => d.MilligramsPerTablets == drugInformation.MilligramsPerTablets && d.NumberOfTablets == drugInformation.NumberOfTablets &&
                        d.SubstancesName == drugInformation.SubstancesName && d.DrugsName == drugInformation.DrugsName));

                    if (drugInformationFromDatabase is null)
                    {
                        context.AddFailure(string.Format("{0} {1} {2} {3} doesn't exist in list of drugs. Please check this position",
                            drugInformation.DrugsName, drugInformation.SubstancesName, drugInformation.NumberOfTablets, drugInformation.MilligramsPerTablets));
                    }
                });

            RuleFor(o => o)
                .Custom((order, context) =>
                {              
                    if(order.IsPersonalPickup)
                    {
                        bool hasAPrescriptionDrug = false;
                        foreach (var item in order.DrugDtos)
                        {
                            var drugInformationFromDatabase = dbContext.DrugInformations
                            .FirstOrDefault((d => d.MilligramsPerTablets == item.MilligramsPerTablets && d.NumberOfTablets == item.NumberOfTablets &&
                            d.SubstancesName == item.SubstancesName && d.DrugsName == item.DrugsName));

                            if (drugInformationFromDatabase != null && drugInformationFromDatabase.PrescriptionRequired == true)
                            {
                                hasAPrescriptionDrug = true;
                                break;
                            }
                        }
                        if(hasAPrescriptionDrug)
                        {
                            context.AddFailure($"Prescription drugs can only be picked up at a pharmacy");
                        }
                    }
                });
        }
    }
}
