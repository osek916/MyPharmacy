using AutoMapper;
using Microsoft.Extensions.Logging;
using MyPharmacy.Entities;
using MyPharmacy.Exceptions;
using MyPharmacy.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyPharmacy.Services
{
    public interface IDrugCategoryService
    {
        public PagedResult<DrugCategoryDto> GetAll(DrugCategoryGetAllQuery query);
        public int Create(CreateDrugCategoryDto dto);
        public void DeleteById(int id);
        public void UpdateById(UpdateDrugCategoryDto dto, int drugCategoryId);
    }

    public class DrugCategoryService : IDrugCategoryService
    {
        private readonly PharmacyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public DrugCategoryService(PharmacyDbContext dbContext, IMapper mapper, ILogger<DrugCategoryService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public PagedResult<DrugCategoryDto> GetAll(DrugCategoryGetAllQuery query)
        {
            IQueryable<DrugCategory> drugCategories = _dbContext
                .DrugCategories;

            if (query.GetByChar != '0')
                drugCategories = drugCategories.Where(d => d.CategoryName.StartsWith(query.GetByChar.ToString()));

            if (query.SortDirection == SortDirection.ASC)
                drugCategories.OrderBy(d => d.CategoryName);
            else
                drugCategories.OrderByDescending(d => d.CategoryName);
      

            var finalDrugCategories = drugCategories
               .Skip((query.PageNumber - 1) * query.PageSize)
               .Take(query.PageSize).ToList();
            var totalItemsCount = drugCategories.Count();
            var drugCategoriesDtos = _mapper.Map<List<DrugCategoryDto>>(finalDrugCategories);

            var result = new PagedResult<DrugCategoryDto>(drugCategoriesDtos, totalItemsCount, query.PageSize, query.PageNumber);
            return result;
        }

        public int Create(CreateDrugCategoryDto dto)
        {
            var drugCategory = _mapper.Map<DrugCategory>(dto);
            _dbContext.DrugCategories.Add(drugCategory);
            _dbContext.SaveChanges();
            return drugCategory.Id;
        }


        public void UpdateById(UpdateDrugCategoryDto dto, int drugCategoryId)
        {
            if (drugCategoryId < 0)
                throw new BadRequestException($"DrugCategory Id must be greater than 0");

            var drugCategory = _dbContext
                .DrugCategories
                .FirstOrDefault(d => d.Id == drugCategoryId);

            if (drugCategory is null)
                throw new NotFoundException($"DrugInformation with id: {drugCategoryId} not found");

            drugCategory.CategoryName = dto.CategoryName;
            drugCategory.Description = dto.Description;
            _dbContext.SaveChanges();
        }


        public void DeleteById(int id)
        {
            var drugCategory = _dbContext
                .DrugCategories
                .FirstOrDefault(d => d.Id == id);

            if(drugCategory is null)
            {
                throw new NotFoundException($"DrugCategory with id: {id} not found");
            }
            _dbContext.DrugCategories.Remove(drugCategory);
            _dbContext.SaveChanges();
        }      
    }
}
