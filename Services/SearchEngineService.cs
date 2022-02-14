using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPharmacy.Entities;
using MyPharmacy.Exceptions;
using MyPharmacy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Services
{
    public interface ISearchEngineService
    {
        PagedResult<SearchEnginePharmacyDto> GetPharmacies(SearchEnginePharmacyQuery query);
        PagedResult<SearchEngineDrugInformationDto> GetDrugInformations(SearchEngineDrugInformationQuery query);
    }

    public class SearchEngineService : ISearchEngineService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<SearchEngineService> _logger;
        private readonly PharmacyDbContext _dbContext;

        public SearchEngineService( IMapper mapper, ILogger<SearchEngineService> logger, PharmacyDbContext dbContext)
        {
            _mapper = mapper;
            _logger = logger;
            _dbContext = dbContext;
        }

        public PagedResult<SearchEngineDrugInformationDto> GetDrugInformations(SearchEngineDrugInformationQuery query)
        {

            var baseQuery = _dbContext
                .DrugInformations
                .Include(d => d.DrugCategories)
                .Where(d => query.Phrase == null || (d.DrugsName.ToLower().Contains(query.Phrase.ToLower()) ||
                d.SubstancesName.ToLower().Contains(query.Phrase.ToLower()))).Where(d => d.PrescriptionRequired == query.PrescriptionRequired);

            if (query.DrugSortBy == DrugSortBy.DrugName)
                baseQuery.OrderBy(d => d.DrugsName);
            else
                baseQuery.OrderBy(d => d.SubstancesName);

            var drugs = baseQuery
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize).ToList();
            var totalItemsCount = baseQuery.Count();
            var drugInformationDtos = _mapper.Map<List<SearchEngineDrugInformationDto>>(drugs);
            var result = new PagedResult<SearchEngineDrugInformationDto>(drugInformationDtos, totalItemsCount, query.PageSize, query.PageNumber);
            return result;

        }

        public PagedResult<SearchEnginePharmacyDto> GetPharmacies(SearchEnginePharmacyQuery query)
        {
            var baseQuery = _dbContext
                .Pharmacies
                .Include(p => p.Address)
                .Where(p => query.Phrase == null || (p.Address.City.ToLower().Contains(query.Phrase.ToLower()) || 
                p.Name.ToLower().Contains(query.Phrase.ToLower())) && p.HasPresciptionDrugs == query.HasPresciptionDrugs);

            if (query.PharmaciesSortBy == PharmaciesSortBy.City)
                baseQuery.OrderBy(p => p.Address.City);
            else 
                baseQuery.OrderBy(p => p.Name);

            var pharmacies = baseQuery
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize).ToList();
            var totalItemsCount = baseQuery.Count();
            var pharmacyDtos = _mapper.Map<List<SearchEnginePharmacyDto>>(pharmacies);

            var result = new PagedResult<SearchEnginePharmacyDto>(pharmacyDtos, totalItemsCount, query.PageSize, query.PageNumber);
            return result;
        }






    }
}
