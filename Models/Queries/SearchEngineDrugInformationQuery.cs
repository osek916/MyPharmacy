using MyPharmacy.Entities;
using MyPharmacy.Models.Interfaces;

namespace MyPharmacy.Models
{
    public class SearchEngineDrugInformationQuery : SortParameters, IPagination, ISortByDirection, ISortBy
    {
        public string SortBy { get; set; } = nameof(DrugInformation.DrugsName);
    }
}

