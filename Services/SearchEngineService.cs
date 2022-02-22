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
        PagedResult<SearchEnginePharmacyDto> GetPharmaciesWithDrugs(SearchEngineDrugQuery query);
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
        public PagedResult<SearchEnginePharmacyDto> GetPharmaciesWithDrugs(SearchEngineDrugQuery query)
        {           
            var baseQuery = _dbContext
                .Pharmacies
                .Include(a => a.Address)
                .Include(d => d.Drugs)
                .ThenInclude(d => d.DrugInformation)
                .Where(d => d.Drugs.Any(dd => dd.DrugInformation.DrugsName.ToLower().Contains(query.Phrase.ToLower()) 
                || dd.DrugInformation.SubstancesName.ToLower().Contains(query.Phrase.ToLower())));

            if (query.City != null)
            {
                baseQuery = baseQuery.Where(d => d.Address.City.ToLower().Contains(query.City.ToLower()));
            }
            if (query.PharmaciesSortBy == PharmaciesSortBy.Name)
            {              
                if (query.SortDirection == SortDirection.ASC)
                    baseQuery.OrderBy(d => d.Name);
                else
                    baseQuery.OrderByDescending(d => d.Name);
            }
            else
            {
                if (query.SortDirection == SortDirection.ASC)
                    baseQuery.OrderBy(d => d.Address.City);
                else
                    baseQuery.OrderByDescending(d => d.Address.City);
            }

            var pharmacies = baseQuery
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize).ToList();
            var totalItemsCount = baseQuery.Count();
            var pharmacyDtos = _mapper.Map<List<SearchEnginePharmacyDto>>(pharmacies);

            var result = new PagedResult<SearchEnginePharmacyDto>(pharmacyDtos, totalItemsCount, query.PageSize, query.PageNumber);
            return result;

        }

        public PagedResult<SearchEngineDrugInformationDto> GetDrugInformations(SearchEngineDrugInformationQuery query)
        {

            var baseQuery = _dbContext
                .DrugInformations
                .Include(d => d.DrugCategories)
                .Where(d => query.Phrase == null || (d.DrugsName.ToLower().Contains(query.Phrase.ToLower()) ||
                d.SubstancesName.ToLower().Contains(query.Phrase.ToLower()))).Where(d => d.PrescriptionRequired == query.PrescriptionRequired);

            if (query.DrugSortBy == DrugSortBy.DrugName)
            {
                if (query.GetByChar != '0')
                    baseQuery = baseQuery.Where(d => d.DrugsName.StartsWith(query.GetByChar.ToString()));

                if (query.SortDirection == SortDirection.ASC)
                    baseQuery.OrderBy(d => d.DrugsName);
                else
                    baseQuery.OrderByDescending(d => d.DrugsName);
            }
            else
            {
                if (query.GetByChar != '0')
                    baseQuery = baseQuery.Where(d => d.SubstancesName.StartsWith(query.GetByChar.ToString()));

                if (query.SortDirection == SortDirection.ASC)
                    baseQuery.OrderBy(d => d.SubstancesName);
                else
                    baseQuery.OrderByDescending(d => d.SubstancesName);
            }

            var drugInformations = baseQuery
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize).ToList();
            var totalItemsCount = baseQuery.Count();
            var drugInformationDtos = _mapper.Map<List<SearchEngineDrugInformationDto>>(drugInformations);
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

            if (query.PharmaciesSortBy == PharmaciesSortBy.Name)
            {
                if (query.GetByChar != '0')
                    baseQuery = baseQuery.Where(p => p.Name.StartsWith(query.GetByChar.ToString()));

                if (query.SortDirection == SortDirection.ASC)
                    baseQuery.OrderBy(d => d.Name);
                else
                    baseQuery.OrderByDescending(d => d.Name);
            }
            else
            {
                if (query.GetByChar != '0')
                    baseQuery = baseQuery.Where(d => d.Address.City.StartsWith(query.GetByChar.ToString()));

                if (query.SortDirection == SortDirection.ASC)
                    baseQuery.OrderBy(d => d.Address.City);
                else
                    baseQuery.OrderByDescending(d => d.Address.City);
            }

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
