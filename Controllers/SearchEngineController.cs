using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyPharmacy.Models;
using MyPharmacy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        //Zwraca same apteki wedle zapytania
        //Działa
        [HttpGet("pharmacy")]
        public ActionResult<PagedResult<SearchEnginePharmacyDto>> GetPharmacies([FromQuery] SearchEnginePharmacyQuery query)
        {
            var searchEnginePharmacyDtos = _searchEngineService.GetPharmacies(query);
            return Ok(searchEnginePharmacyDtos);
        }

        //Wyszukuje konkretnych informacji o leku
        //poprawić zwracaną listę
        [HttpGet("druginformation")]
        public ActionResult<PagedResult<SearchEngineDrugInformationDto>> GetDrugInformations([FromQuery] SearchEngineDrugInformationQuery query)
        {
            var searchEngineDrugInformationDtos = _searchEngineService.GetDrugInformations(query);
            return Ok(searchEngineDrugInformationDtos);
        }
         
        
        [HttpGet("drug")]
        public ActionResult<PagedResult<SearchEngineDrugDto>> GetPharmaciesWithDrug([FromQuery] SearchEngineDrugQuery query)
        {
            var searchEngineDrugDtos = _searchEngineService.GetPharmaciesWithDrugs(query);
            return Ok(searchEngineDrugDtos);
        }
          

    }
}

