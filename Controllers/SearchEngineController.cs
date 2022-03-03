using Microsoft.AspNetCore.Mvc;
using MyPharmacy.Models;
using MyPharmacy.Services;

namespace MyPharmacy.Controllers
{
    [Route("api/searchengine")]
    [ApiController]
    public class SearchEngine : ControllerBase
    {
        private readonly ISearchEngineService _searchEngineService;

        public SearchEngine(ISearchEngineService searchEngineService)
        {
            _searchEngineService = searchEngineService;
        }
        
        //searchengine only returns non-sensitive data that can be customized during application development
            
        [HttpGet("pharmacy")]
        public ActionResult<PagedResult<SearchEnginePharmacyDto>> GetPharmacies([FromQuery] SearchEnginePharmacyQuery query)
        {
            var searchEnginePharmacyDtos = _searchEngineService.GetPharmacies(query);
            return Ok(searchEnginePharmacyDtos);
        }


        [HttpGet("druginformation")]
        public ActionResult<PagedResult<SearchEngineDrugInformationDto>> GetDrugInformations([FromQuery] SearchEngineDrugInformationQuery query)
        {
            var searchEngineDrugInformationDtos = _searchEngineService.GetDrugInformations(query);
            return Ok(searchEngineDrugInformationDtos);
        }
         
        
        [HttpGet("pharmacywithdrug")]
        public ActionResult<PagedResult<SearchEngineDrugDto>> GetPharmaciesWithDrug([FromQuery] SearchEngineDrugQuery query)
        {
            var searchEngineDrugDtos = _searchEngineService.GetPharmaciesWithDrugs(query);
            return Ok(searchEngineDrugDtos);
        }         
    }
}

