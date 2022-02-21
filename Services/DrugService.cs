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
    public interface IDrugService
    {
        PagedResult<DrugDto> GetAll(int pharmacyId, DrugGetAllQuery query);
        DrugDto GetById(int pharmacyId, int drugId);
        int Create(int pharmacyId, CreateDrugDto dto);
        void DeletedById(int pharmacyId, int drugId);
        void DeletedAllDrugsPharmacyWithId(int pharmacyId); 
        void Update(int pharmacyId,  UpdateDrugDto dto);
        //IEnumerable<DrugDto> GetAllByCategory(int pharmacyId, DrugQuery query);


       // IEnumerable<DrugDto> GetAllByNameOfSubstance(int pharmacyId, string nameOfSubstance);
       // IEnumerable<DrugDto> GetAllByNameOfDrug(int pharmacyId, string nameOfDrug);

    }

    public class DrugService : IDrugService
    {
        private readonly PharmacyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IUserContextService _userContextService;

        public DrugService(PharmacyDbContext context, IMapper mapper, ILogger<DrugService> logger, IUserContextService userContextService)
        {
            _dbContext = context;
            _mapper = mapper;
            _logger = logger;
            _userContextService = userContextService;
        }

        public PagedResult<DrugDto> GetAll(int pharmacyId, DrugGetAllQuery query)
        {
            if (pharmacyId < 1)
            {
                throw new BadRequestException("Drug id must be greater than 0");
            }
            if (_userContextService.Role != "Admin")
            {
                if (_userContextService.PharmacyId != pharmacyId)
                {
                    throw new ForbiddenException($"The specified drug doesn't exist or does not belong to your pharmacy");
                }
            }

            var drugs = _dbContext
                .Drugs
                .Include(d => d.DrugInformation)
                .Where(d => d.PharmacyId == pharmacyId && (query.Phrase == null || (d.DrugInformation.DrugsName.ToLower().Contains(query.Phrase.ToLower()) || d.DrugInformation.SubstancesName.ToLower().Contains(query.Phrase.ToLower()))));


            if (query.DrugSortBy == DrugSortBy.DrugName)
            {
                if (query.GetByChar != '0')
                    drugs = drugs.Where(d => d.DrugInformation.DrugsName.StartsWith(query.GetByChar));

                if (query.SortDirection == SortDirection.ASC)
                    drugs.OrderBy(d => d.DrugInformation.DrugsName);
                else
                    drugs.OrderByDescending(d => d.DrugInformation.DrugsName);
            }
            else
            {
                if (query.GetByChar != '0')
                    drugs = drugs.Where(d => d.DrugInformation.SubstancesName.StartsWith(query.GetByChar));

                if (query.SortDirection == SortDirection.ASC)
                    drugs.OrderBy(d => d.DrugInformation.SubstancesName);
                else
                    drugs.OrderByDescending(d => d.DrugInformation.SubstancesName);
            }

            var finalDrugs = drugs
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize).ToList();
            var totalItemsCount = drugs.Count();
            var drugsDtos = _mapper.Map<List<DrugDto>>(finalDrugs);

            var result = new PagedResult<DrugDto>(drugsDtos, totalItemsCount, query.PageSize, query.PageNumber);

            return result;

        }

        public DrugDto GetById(int pharmacyId, int drugId)
        {
            var drug = GetDrugWithAdminPrivilege(drugId);
            return _mapper.Map<DrugDto>(drug);

        }

        public int Create(int pharmacyId, CreateDrugDto dto)
        {
            if (dto.Price < 0 || dto.NumberOfTablets < 1 || dto.MilligramsPerTablets < 0)
                throw new BadRequestException($"Bad drug parameters");

            var drugInformation = _dbContext.DrugInformations.FirstOrDefault(d => dto.DrugsName.ToLower() == d.DrugsName.ToLower() && dto.MilligramsPerTablets == d.MilligramsPerTablets &&
            dto.NumberOfTablets == d.NumberOfTablets && dto.SubstancesName.ToLower() == d.SubstancesName.ToLower());

            

            if (drugInformation is null)
                throw new NotFoundException($"Drug with this information parameters not found. Check the correctness of the entered data");

            var pharmacy = GetPharmacyById(pharmacyId);
            if (pharmacy.HasPresciptionDrugs == false && drugInformation.PrescriptionRequired == true)
            {
                throw new ForbiddenException($"Your pharmacy is not authorized to sell prescription drugs");
            }

            Drug newDrug = new Drug()
            {
                AmountOfPackages = dto.AmountOfPackages,
                Price = dto.Price,
                DrugInformationId = drugInformation.Id
            };

            _dbContext.Drugs.Add(newDrug);
            _dbContext.SaveChanges();
            return newDrug.Id;
        }

        

        public void Update(int pharmacyId, UpdateDrugDto dto)
        {

            var drug = GetDrugWithAdminPrivilege(dto.OptionalId);

            drug.AmountOfPackages = dto.AmountOfPackages;
            drug.Price = dto.Price;

            _dbContext.SaveChanges();
        }

        public void DeletedById(int pharmacyId, int drugId)
        {
            var drug = GetDrugWithAdminPrivilege(drugId);
            //_logger.LogWarning($"Attempt to remove drug id: {drugId} from the pharmacy with id: {pharmacyId}");
            _dbContext.Remove(drug);
            _dbContext.SaveChanges();
        }

        
        
        





        

        

        

        public void DeletedAllDrugsPharmacyWithId(int pharmacyId)
        {
            if (pharmacyId < 1)
            {
                throw new BadRequestException("Drug id must be greater than 0");
            }
            if (_userContextService.Role != "Admin")
            {
                if (_userContextService.PharmacyId != pharmacyId)
                {
                    throw new ForbiddenException($"The specified drug doesn't exist or does not belong to your pharmacy");
                }
            }

            _logger.LogWarning($"Attempt to remove all drugs from the pharmacy with id: {pharmacyId}");
            var drugs = _dbContext.Drugs.Where(d => d.PharmacyId == pharmacyId);
            if(drugs is null)
            {
                throw new NotFoundException($"This pharmacy with id: {pharmacyId} not have any drugs");
            }
            _dbContext.RemoveRange(drugs);
            _dbContext.SaveChanges();
        }

        private Pharmacy GetPharmacyById(int pharmacyId)
        {

            var pharmacy = _dbContext
                .Pharmacies
                .Include(p => p.Drugs)
                .FirstOrDefault(p => p.Id == pharmacyId);

            if (pharmacy is null)
            {
                throw new NotFoundException($"Pharmacy with id: {pharmacyId} not exist");
            }
            return pharmacy;

        }

        public Drug GetDrugWithAdminPrivilege(int drugId)
        {
            if (drugId < 1)
            {
                throw new BadRequestException("Drug id must be greater than 0");
            }

            var drug = _dbContext
                .Drugs
                .FirstOrDefault(d => d.Id == drugId);

            if (_userContextService.Role != "Admin")
            {
                if (_userContextService.PharmacyId != drug.PharmacyId)
                {
                    throw new ForbiddenException($"The specified drug doesn't exist or does not belong to your pharmacy");
                }
            }
            if (drug is null)
            {
                throw new NotFoundException($"Drug with id: {drugId} not found");
            }
            return drug;
        }

    }
}








/*
        public IEnumerable<DrugDto> GetAllByCategory(int pharmacyId, DrugQuery query)
        {
            var temporaryQuery = _dbContext
                .Drugs
            var drugs = _dbContext.
        
        */

/*
public int Create(int pharmacyId, CreateDrugDto dto)
{
    var drugInformation = _dbContext.DrugInformations.FirstOrDefault(d => dto.DrugsName.ToLower() == d.DrugsName.ToLower() && dto.MilligramsPerTablets == d.MilligramsPerTablets &&
    dto.NumberOfTablets == d.NumberOfTablets && dto.SubstancesName.ToLower() == d.SubstancesName.ToLower());

    if (drugInformation is null || dto.Price < 0 || dto.NumberOfTablets < 0 || dto.MilligramsPerTablets < 0)
        throw new NotFoundException($"Drug with this information parameters not found. Chec the correctness of the entered data");

    Drug newDrug = new Drug()
    {
        AmountOfPackages = dto.AmountOfPackages,
        Price = dto.Price,
        DrugInformationId = drugInformation.Id
    };

    _dbContext.Drugs.Add(newDrug);
    _dbContext.SaveChanges();
    return newDrug.Id;
}
*/


/*
        
        public IEnumerable<DrugDto> GetAllByNameOfSubstance(int pharmacyId, string nameOfSubstance)
        {
            var pharmacy = GetPharmacyById(pharmacyId);

            var drugs = _dbContext.Drugs.Where(d => d.SubstancesName == nameOfSubstance && d.PharmacyId == pharmacyId);  
            if(drugs.Count() == 0)
            {
                throw new NotFoundException($"Drugs with this substances: {nameOfSubstance} not found in this Pharmacy");
            }
            var drugDtos = _mapper.Map<List<DrugDto>>(drugs);
            return drugDtos;
        }
        
        public IEnumerable<DrugDto> GetAllByNameOfDrug(int pharmacyId, string nameOfDrug)
        {
            var pharmacy = GetPharmacyById(pharmacyId);
            var drugs = _dbContext.Drugs.Where(d => d.PharmacyId == pharmacyId && d.DrugsName == nameOfDrug);
            if (drugs.Count() == 0)
            {
                throw new NotFoundException($"Drugs with this substances: {nameOfDrug} not found in this Pharmacy");
            }
            var drugDtos = _mapper.Map<List<DrugDto>>(drugs);
            return drugDtos;
        }


        */

/*
        public void Update(int pharmacyId,  UpdateDrugDto dto)
        {

            if(dto.OptionalId < 1)
            {
                throw new BadRequestException("Drug id must be greater than 0");
            }      
            var pharmacy = GetPharmacyById(pharmacyId);
            var drug = _dbContext
                .Drugs
                .FirstOrDefault(d => d.Id == dto.OptionalId);

            if (drug is null)
            {
                throw new NotFoundException($"Drug with id: {dto.OptionalId} not found");
            }
            if()

            drug.AmountOfPackages = dto.AmountOfPackages;
            drug.Price = dto.Price;

            _dbContext.SaveChanges();
        }
        */
/*
public void UpdateDrugById(int pharmacyId, int drugId, UpdateDrugDto dto)
{
    var pharmacy = GetPharmacyById(pharmacyId);
    var drug = _dbContext
        .Drugs
        .FirstOrDefault(d => d.Id == drugId);
    if(drug is null)
    {
        throw new NotFoundException($"Drug with id: {drugId} not found");
    }


    drug.AmountOfPackages = dto.AmountOfPackages;
    drug.Price = dto.Price;


    _dbContext.SaveChanges();
}
*/