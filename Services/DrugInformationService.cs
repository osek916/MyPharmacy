using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPharmacy.Entities;
using MyPharmacy.Exceptions;
using MyPharmacy.Helpers;
using MyPharmacy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyPharmacy.Services
{
    public interface IDrugInformationService
    {
        public PagedResult<DrugInformationDto> GetAll(GetAllDrugInformationQuery query);
        public int Create(CreateDrugInformationDto dto);
        public void DeleteById(int drugInformationId);
        public void UpdateById(UpdateDrugInformationDto dto, int drugInformationId);
    }

    public class DrugInformationService : IDrugInformationService
    {

        private readonly PharmacyDbContext _dbContext;
        private readonly IMapper _mapper;

        public DrugInformationService(PharmacyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public PagedResult<DrugInformationDto> GetAll(GetAllDrugInformationQuery query)
        {
            var drugInformations = _dbContext
                .DrugInformations
                .Where(d => query.Phrase == null || (d.DrugsName.ToLower().Contains(query.Phrase.ToLower()) || d.SubstancesName.ToLower().Contains(query.Phrase.ToLower())));

            var selector = new Dictionary<string, Expression<Func<DrugInformation, object>>>
            {
                {nameof(DrugInformation.DrugsName), d => d.DrugsName},
                {nameof(DrugInformation.SubstancesName), d => d.SubstancesName }
            };

            if (query.SortDirection == SortDirection.ASC)
                drugInformations = drugInformations.OrderBy(selector[query.SortBy]);

            else
                drugInformations = drugInformations.OrderByDescending(selector[query.SortBy]);

            var finaldrugInformations = PaginationHelper<DrugInformation, GetAllDrugInformationQuery>.ReturnPaginatedList(query, drugInformations);
            var drugInformationDtos = _mapper.Map<List<DrugInformationDto>>(finaldrugInformations);

            var result = new PagedResult<DrugInformationDto>(drugInformationDtos, drugInformations.Count(), query.PageSize, query.PageNumber);

            return result;
        }

        public int Create(CreateDrugInformationDto dto)
        {
            var drugInformation = _mapper.Map<DrugInformation>(dto);
            _dbContext.DrugInformations.Add(drugInformation);
            _dbContext.SaveChanges();
            return (drugInformation.Id);
        }


        public void UpdateById(UpdateDrugInformationDto dto, int drugInformationId)
        {
            if (drugInformationId < 0)
                throw new BadRequestException($"DrugInformation Id must be greater than 0");

            var drugInformation = _dbContext
                .DrugInformations
                .Include(d => d.DrugCategories)
                .FirstOrDefault(d => d.Id == drugInformationId);

            if (drugInformation is null)
                throw new NotFoundException($"DrugInformation with id: {drugInformationId} not found");
             
            drugInformation.DrugsName = dto.DrugsName;
            drugInformation.Description = dto.Description;
            drugInformation.LumpSumDrug = dto.LumpSumDrug;
            drugInformation.MilligramsPerTablets = dto.MilligramsPerTablets;
            drugInformation.NumberOfTablets = dto.NumberOfTablets;
            drugInformation.PrescriptionRequired = dto.PrescriptionRequired;
            drugInformation.SubstancesName = drugInformation.SubstancesName;

            _dbContext.SaveChanges();
        }

        public void DeleteById(int drugInformationId)
        {
            var drugInformation = _dbContext
                .DrugInformations
                .FirstOrDefault(d => d.Id == drugInformationId);

            if(drugInformation is null)
                throw new NotFoundException($"DrugInformation with id: {drugInformationId} not found");

            _dbContext.DrugInformations.Remove(drugInformation);
            _dbContext.SaveChanges();
        }       
    }
}
