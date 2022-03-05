using MyPharmacy.Entities;
using MyPharmacy.Models.Interfaces;

namespace MyPharmacy.Models
{
    public class GetAllDrugInformationQuery : SortParameters, ISortByDirection, IPagination
    {
        public string SortBy { get; set; } = nameof(DrugInformation.DrugsName);
    }
}
