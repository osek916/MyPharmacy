using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPharmacy.Entities;
using MyPharmacy.Helpers;
using MyPharmacy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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
        private readonly PharmacyDbContext _dbContext;

        public SearchEngineService( IMapper mapper, PharmacyDbContext dbContext)
        {
            _mapper = mapper;
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
                baseQuery = baseQuery.Where(d => d.Address.City.ToLower().Contains(query.City.ToLower()));
            
            var selector = new Dictionary<string, Expression<Func<Pharmacy, object>>>
            {
                {nameof(Pharmacy.Name), p => p.Name},
                {nameof(Pharmacy.Address.City), p => p.Address.City }
            };

            if (query.SortDirection == SortDirection.ASC)
                baseQuery = baseQuery.OrderBy(selector[query.SortBy]);

            else
                baseQuery = baseQuery.OrderByDescending(selector[query.SortBy]);

            var pharmacies = PaginationHelper<Pharmacy, SearchEngineDrugQuery>.ReturnPaginatedList(query, baseQuery);
            var pharmacyDtos = _mapper.Map<List<SearchEnginePharmacyDto>>(pharmacies);
            var result = new PagedResult<SearchEnginePharmacyDto>(pharmacyDtos, baseQuery.Count(), query.PageSize, query.PageNumber);

            return result;
        }


        public PagedResult<SearchEngineDrugInformationDto> GetDrugInformations(SearchEngineDrugInformationQuery query)
        {
            var baseQuery = _dbContext
                .DrugInformations
                .Include(d => d.DrugCategories)
                .Where(d => query.Phrase == null || (d.DrugsName.ToLower().Contains(query.Phrase.ToLower()) ||
                d.SubstancesName.ToLower().Contains(query.Phrase.ToLower())));

            var selector = new Dictionary<string, Expression<Func<DrugInformation, object>>>
            {
                {nameof(DrugInformation.DrugsName), d => d.DrugsName},
                {nameof(DrugInformation.SubstancesName), d => d.SubstancesName }
            };

            if (query.SortDirection == SortDirection.ASC)
                baseQuery = baseQuery.OrderBy(selector[query.SortBy]);

            else
                baseQuery = baseQuery.OrderByDescending(selector[query.SortBy]);

            var drugInformations = PaginationHelper<DrugInformation,SearchEngineDrugInformationQuery>.ReturnPaginatedList(query, baseQuery);
            var drugInformationDtos = _mapper.Map<List<SearchEngineDrugInformationDto>>(drugInformations);
            var result = new PagedResult<SearchEngineDrugInformationDto>(drugInformationDtos, baseQuery.Count(), query.PageSize, query.PageNumber);

            return result;
        }    

        public PagedResult<SearchEnginePharmacyDto> GetPharmacies(SearchEnginePharmacyQuery query)
        {
            var baseQuery = _dbContext
                .Pharmacies
                .Include(p => p.Address)
                .Where(p => query.Phrase == "" || (p.Address.City.ToLower().Contains(query.Phrase.ToLower()) || 
                p.Name.ToLower().Contains(query.Phrase.ToLower())) && p.HasPresciptionDrugs == query.HasPresciptionDrugs);

            var selector = new Dictionary<string, Expression<Func<Pharmacy, object>>>
            {
                {nameof(Pharmacy.Name), p => p.Name},
                {nameof(Pharmacy.Address.City), p => p.Address.City }
            };
            
            if (query.SortDirection == SortDirection.ASC)
                baseQuery = baseQuery.OrderBy(selector[query.SortBy]);

            else
                baseQuery = baseQuery.OrderByDescending(selector[query.SortBy]);
            
            var pharmacies = PaginationHelper<Pharmacy, SearchEnginePharmacyQuery>.ReturnPaginatedList(query, baseQuery);
            var pharmacyDtos = _mapper.Map<List<SearchEnginePharmacyDto>>(pharmacies);
            var result = new PagedResult<SearchEnginePharmacyDto>(pharmacyDtos, baseQuery.Count(), query.PageSize, query.PageNumber);

            return result;
        }
    }
}

