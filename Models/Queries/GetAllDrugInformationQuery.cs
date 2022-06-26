using MyPharmacy.Entities;
using MyPharmacy.Models.Interfaces;

namespace MyPharmacy.Models
{
    public class GetAllDrugInformationQuery : SortParameters, ISortByDirection, IPagination, IPhrase
    {
        public string SortBy { get; set; } = nameof(DrugInformation.DrugsName);
        public string Phrase { get; set; } = "";
    }
}
