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
        private readonly ILogger _logger;

        public DrugInformationService(PharmacyDbContext dbContext, IMapper mapper, ILogger<DrugInformationService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public PagedResult<DrugInformationDto> GetAll(GetAllDrugInformationQuery query)
        {
            var drugInformations = _dbContext
                .DrugInformations
                .Where(d => query.Phrase == null || (d.DrugsName.ToLower().Contains(query.Phrase.ToLower()) || d.SubstancesName.ToLower().Contains(query.Phrase.ToLower())));


            if (query.DrugSortBy == DrugSortBy.DrugName)
            {
                if (query.GetByChar != '0')
                    drugInformations = drugInformations.Where(d => d.DrugsName.StartsWith(query.GetByChar));

                if (query.SortDirection == SortDirection.ASC)
                    drugInformations.OrderBy(d => d.DrugsName);
                else
                    drugInformations.OrderByDescending(d => d.DrugsName);
            }
            else
            {
                if (query.GetByChar != '0')
                    drugInformations = drugInformations.Where(d => d.SubstancesName.StartsWith(query.GetByChar));

                if (query.SortDirection == SortDirection.ASC)
                    drugInformations.OrderBy(d => d.SubstancesName);
                else
                    drugInformations.OrderByDescending(d => d.SubstancesName);
            }

            var finaldrugInformations = drugInformations
               .Skip((query.PageNumber - 1) * query.PageSize)
               .Take(query.PageSize).ToList();
            var totalItemsCount = finaldrugInformations.Count();
            var pharmaciesDto = _mapper.Map<List<DrugInformationDto>>(finaldrugInformations);

            var result = new PagedResult<DrugInformationDto>(pharmaciesDto, totalItemsCount, query.PageSize, query.PageNumber);

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

            var temporaryDrugCategories = new List<DrugCategory>();
            if (dto.DrugCategoryNames.Count() != 0 && !(dto.DrugCategoryNames is null))
            {
                foreach (var item in dto.DrugCategoryNames)
                {
                    var categoryWithThisName = _dbContext.DrugCategories.FirstOrDefault(d => d.CategoryName.ToLower().Contains(item.ToLower()));
                    if (!(categoryWithThisName is null))
                    {
                        temporaryDrugCategories.Add(categoryWithThisName);
                    }
                }
                if (temporaryDrugCategories.Count != 0)
                {
                    drugInformation.DrugCategories = new List<DrugCategory>(temporaryDrugCategories);
                    drugInformation.DrugCategories.AddRange(temporaryDrugCategories);
                }
            }

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
