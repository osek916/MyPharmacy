using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPharmacy.Entities;
using MyPharmacy.Exceptions;
using MyPharmacy.Models;
using MyPharmacy.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyPharmacy.Services
{
    public interface IPharmacyService
    {
        PagedResult<PharmacyDto> GetAll(PharmacyGetAllQuery query);
        PharmacyDto GetOne(int id);
        int Create(CreatePharmacyDto dto);
        void Update(UpdatePharmacyDto dto);
        void Delete(int id);

    }

    public class PharmacyService : IPharmacyService
    {
        private readonly PharmacyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IUserContextService _userContextService;

        public PharmacyService(PharmacyDbContext dbContext, IMapper mapper, ILogger<PharmacyService> logger, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _userContextService = userContextService;
        }

       
        
        
        public PagedResult<PharmacyDto> GetAll(PharmacyGetAllQuery query)
        {
            var pharmacies = _dbContext
                .Pharmacies
                .Include(x => x.Address)
                .Where(d => query.Phrase == null || (d.Address.City.ToLower().Contains(query.Phrase.ToLower()) || d.Name.ToLower().Contains(query.Phrase.ToLower())));


            if (query.PharmaciesSortBy == PharmaciesSortBy.Name)
            {
                if (query.GetByChar != '0')
                    pharmacies = pharmacies.Where(p => p.Name.StartsWith(query.GetByChar));

                if (query.SortDirection == SortDirection.ASC)
                    pharmacies.OrderBy(d => d.Name);
                else
                    pharmacies.OrderByDescending(d => d.Name);
            }
            else
            {
                if (query.GetByChar != '0')
                    pharmacies = pharmacies.Where(d => d.Address.City.StartsWith(query.GetByChar));

                if (query.SortDirection == SortDirection.ASC)
                    pharmacies.OrderBy(d => d.Address.City);
                else
                    pharmacies.OrderByDescending(d => d.Address.City);
            }

            var finalPharmacies = pharmacies
               .Skip((query.PageNumber - 1) * query.PageSize)
               .Take(query.PageSize).ToList();
            var totalItemsCount = pharmacies.Count();
            var pharmaciesDto = _mapper.Map<List<PharmacyDto>>(finalPharmacies);

            var result = new PagedResult<PharmacyDto>(pharmaciesDto, totalItemsCount, query.PageSize, query.PageNumber);

            return result;
        }

       /*
        public IQueryable<T> SortedBy<T, T1, T2>(IQueryable<T> data, Expression<Func<T1, object>> sortByLambdaExpression, T2 query) where T2 : ISortByDirection
        {
            if (query.SortDirection != SortDirection.ASC)
            {
                query = data.OrderBy(sortByLambdaExpression);
            }
            Expression<Func<T1, object>> aaaa = r => r.Nam
        }
       */
       
       
        public PharmacyDto GetOne(int id)
        {
            if (id < 1)
            {
                throw new BadRequestException($"Pharmacy id must be greater than {id}");
            }
            var pharmacy = _dbContext
                .Pharmacies
                .Include(x => x.Address)
                .Where(x => x.Id == id)
                .FirstOrDefault();

            if (pharmacy is null)
            {
                throw new NotFoundException($"Pharmacy with {id} not found");
            }

            var pharmacyDto = _mapper.Map<PharmacyDto>(pharmacy);
            return pharmacyDto;
        }

        public int Create(CreatePharmacyDto dto)
        {
            var pharmacy = _mapper.Map<Pharmacy>(dto);

            var managerHasOnePharmacy = _dbContext.Pharmacies.Any(p => p.CreatedByUserId == _userContextService.GetUserId);
            if (managerHasOnePharmacy)
                throw new ForbiddenException($"You have already a Pharmacy");


            //Konto usunięte w trakcie bycia zalogowanym
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == _userContextService.GetUserId);
            if (user is null)
            {
                throw new ForbiddenException($"Your account doesn't exist");
            }

            pharmacy.CreatedByUserId = _userContextService.GetUserId;

            _dbContext.Add(pharmacy);
            _dbContext.SaveChanges();

            return pharmacy.Id;
        }

        public void Update(UpdatePharmacyDto dto)
        {
            var pharmacy = GetPharmacyByCreatedUserForAdminAndManager(dto.OptionalPharmacyId);

            pharmacy.Name = dto.Name;
            pharmacy.ContactNumber = dto.ContactNumber;
            pharmacy.HasPresciptionDrugs = dto.HasPresciptionDrugs;
            pharmacy.ContactEmail = dto.ContactEmail;
            pharmacy.Address.City = dto.City;
            pharmacy.Address.Street = dto.Street;
            pharmacy.Address.PostalCode = dto.PostalCode;
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            _logger.LogWarning($"Attempt to remove pharmacy id: {id}");

            var pharmacy = GetPharmacyByCreatedUserForAdminAndManager(id);
            _dbContext.Pharmacies.Remove(pharmacy);
            _dbContext.SaveChanges();
        }
        

        private Pharmacy GetPharmacyByCreatedUserForAdminAndManager(int id)
        {
            Pharmacy pharmacy;
            var pharmacies = _dbContext
                .Pharmacies
                .Include(p => p.Drugs)
                .Include(p => p.Address);

            if (_userContextService.Role == "Admin")
            {
                if (id > 0)
                {
                    pharmacy = pharmacies.FirstOrDefault(p => p.Id == id);
                }
                else
                    throw new BadRequestException($"Pharmacy Id must be greater than 0");
            }
            else
            {
                pharmacy = pharmacies.FirstOrDefault(p => p.CreatedByUserId == _userContextService.GetUserId);

            }
            if (pharmacy is null)
            {
                throw new NotFoundException($"Pharmacy not found");
            }

            return pharmacy;
        }     
    }


}

/*
 * 
 public PagedResult<PharmacyDto> GetAll(PharmacyGetAllQuery query)
        {
            var pharmacies = _dbContext
                .Pharmacies
                .Include(x => x.Address)
                .Where(d => query.Phrase == null || (d.Address.City.ToLower().Contains(query.Phrase.ToLower()) || d.Name.ToLower().Contains(query.Phrase.ToLower())));


            if (query.PharmaciesSortBy == PharmaciesSortBy.Name)
            {
                if (query.GetByChar != '0')
                    pharmacies = pharmacies.Where(p => p.Name.StartsWith(query.GetByChar));

                if (query.SortDirection == SortDirection.ASC)
                    pharmacies.OrderBy(d => d.Name);
                else
                    pharmacies.OrderByDescending(d => d.Name);
            }
            else
            {
                if (query.GetByChar != '0')
                    pharmacies = pharmacies.Where(d => d.Address.City.StartsWith(query.GetByChar));

                if (query.SortDirection == SortDirection.ASC)
                    pharmacies.OrderBy(d => d.Address.City);
                else
                    pharmacies.OrderByDescending(d => d.Address.City);
            }

            var finalPharmacies = pharmacies
               .Skip((query.PageNumber - 1) * query.PageSize)
               .Take(query.PageSize).ToList();
            var totalItemsCount = finalPharmacies.Count();
            var pharmaciesDto = _mapper.Map<List<PharmacyDto>>(finalPharmacies);

            var result = new PagedResult<PharmacyDto>(pharmaciesDto, totalItemsCount, query.PageSize, query.PageNumber);

            return result;
        }
*/





/*
          public void Update(UpdatePharmacyDto dto)
          {
              int? Id = 0;
              if(_userContextService.Role == "Admin")
              {
                  if (dto.OptionalPharmacyId > 0)
                  {
                      Id = dto.OptionalPharmacyId;
                  }
                  else
                      throw new BadRequestException($"Pharmacy Id must be greater than 0");   
              }
              else
              {
                  Id = _userContextService.GetUserId;
                  if(Id is null)
                  {
                      throw new NotFoundException($"The Manager didn't create any pharmacy");
                  }
              }
              var pharmacy = _dbContext
                  .Pharmacies
                  .Include(x => x.Address)
                  .FirstOrDefault(p => p.CreatedByUserId == Id);

              if (pharmacy is null)
              {
                  throw new NotFoundException($"Pharmacy with {Id} not found");
              }


              pharmacy.Name = dto.Name;
              pharmacy.ContactNumber = dto.ContactNumber;
              pharmacy.HasPresciptionDrugs = dto.HasPresciptionDrugs;
              pharmacy.ContactEmail = dto.ContactEmail;
              pharmacy.Address.City = dto.City;
              pharmacy.Address.Street = dto.Street;
              pharmacy.Address.PostalCode = dto.PostalCode;
              _dbContext.SaveChanges();
          }*/

/*
        public IEnumerable<DrugDto> GetAllByNameOfSubstance(string nameOfSubstance)
        {
            
            var drugs = _dbContext.Drugs
                .Include(d => d.DrugInformation).Where(d => d.DrugInformation.SubstancesName == nameOfSubstance);
            if(drugs.Count() == 0)
            {
                throw new NotFoundException($"Drugs with name of substances: {nameOfSubstance} not exist");
            }
            var drugsDto = _mapper.Map<List<DrugDto>>(drugs);
            return drugsDto;
            
         
        }
        */
/*
public void Delete(int id)
{

    _logger.LogWarning($"Attempt to remove pharmacy id: {id}");

    Pharmacy pharmacy;
    var pharmacies = _dbContext
        .Pharmacies
        .Include(p => p.Drugs)
        .Include(p => p.Address);

    if (_userContextService.Role == "Admin")
    {
        if (id > 0)
        {
            pharmacy = pharmacies.FirstOrDefault(p => p.Id == id);
        }
        else
            throw new BadRequestException($"Pharmacy Id must be greater than 0");
    }
    else
    {
        pharmacy = pharmacies.FirstOrDefault(p => p.CreatedByUserId == _userContextService.GetUserId);

    }
    if (pharmacy is null)
    {
        throw new NotFoundException($"Pharmacy not found");
    }

    _dbContext.Pharmacies.Remove(pharmacy);
    _dbContext.SaveChanges();

}

public void Update(UpdatePharmacyDto dto)
{
    Pharmacy pharmacy;
    var pharmacies = _dbContext
        .Pharmacies
        .Include(p => p.Address);

    if (_userContextService.Role == "Admin")
    {
        if (dto.OptionalPharmacyId > 0)
        {
            pharmacy = pharmacies.FirstOrDefault(p => p.Id == dto.OptionalPharmacyId);
        }
        else
            throw new BadRequestException($"Pharmacy Id must be greater than 0");
    }
    else
    {
        pharmacy = pharmacies.FirstOrDefault(p => p.CreatedByUserId == _userContextService.GetUserId);

    }
    if (pharmacy is null)
    {
        throw new NotFoundException($"Pharmacy not found");
    }
    pharmacy.Name = dto.Name;
    pharmacy.ContactNumber = dto.ContactNumber;
    pharmacy.HasPresciptionDrugs = dto.HasPresciptionDrugs;
    pharmacy.ContactEmail = dto.ContactEmail;
    pharmacy.Address.City = dto.City;
    pharmacy.Address.Street = dto.Street;
    pharmacy.Address.PostalCode = dto.PostalCode;
    _dbContext.SaveChanges();
}
*/

/*public IEnumerable<DrugDto> GetAllByCategory(DrugQuery query)
       {

           var temporaryQuery = _dbContext
               .Drugs
               .Include(d => d.DrugInformation)
               .Include(d => d.Pharmacy)
               .ThenInclude(a => a.Address)
               .Where(d => query.Phrase == null || (d.DrugCategory.ToLower().Contains(query.Phrase.ToLower()) || d.DrugsName.ToLower().Contains(query.Phrase.ToLower())));

           if(!string.IsNullOrEmpty(query.City))
               temporaryQuery = temporaryQuery.Where(d => d.Pharmacy.Address.City == query.City);


           var sortCategory = new Dictionary<string, Expression<Func<Drug, object>>>
           {
               {nameof(Drug.DrugCategory), d => d.DrugCategory },
               {nameof(Drug.DrugsName), d =>d.DrugsName },
               {nameof(Drug.Price), d => d.Price },
               {nameof(Drug.SubstancesName), d => d.SubstancesName }
           };


           if(query.SortDirection == SortDirection.DESC)
           {
               temporaryQuery.OrderByDescending(sortCategory[query.SortCategory]);
           }
           else
           {
               temporaryQuery.OrderBy(sortCategory[query.SortCategory]);
           }
           var drugs = temporaryQuery.Skip(query.NumberPositionsOnPage * (query.ActualPage - 1))
               .Take(query.NumberPositionsOnPage).ToList();
           var drugsDtos = _mapper.Map<List<DrugDto>>(drugs);

           //var result = new 


       }*/