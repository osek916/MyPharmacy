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
        List<DrugDto> GetAll(int pharmacyId);
        DrugDto GetById(int pharmacyId, int drugId);
        int Create(int pharmacyId, CreateDrugDto dto);
        void DeletedById(int pharmacyId, int drugId);
        void DeletedAllDrugsPharmacyWithId(int pharmacyId); 
        void UpdateDrugById(int pharmacyId, int drugId, UpdateDrugDto dto);
        
        IEnumerable<DrugDto> GetAllByNameOfSubstance(int pharmacyId, string nameOfSubstance);

    }

    public class DrugService : IDrugService
    {
        private readonly PharmacyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public DrugService(PharmacyDbContext context, IMapper mapper, ILogger<DrugService> logger)
        {
            _dbContext = context;
            _mapper = mapper;
            _logger = logger;
        }

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

            drug.DrugsName = dto.DrugsName;
            drug.DrugCategory = dto.DrugCategory;
            drug.AmountOfPackages = dto.AmountOfPackages;
            drug.LumpSumDrug = dto.LumpSumDrug;
            drug.MilligramsPerTablets = dto.MilligramsPerTablets;
            drug.NumberOfTablets = dto.NumberOfTablets;
            drug.PrescriptionRequired = dto.PrescriptionRequired;
            drug.Price = dto.Price;
            drug.SubstancesName = dto.SubstancesName;

            _dbContext.SaveChanges();
        }

        public List<DrugDto> GetAll(int pharmacyId)
        {
            var pharmacy = GetPharmacyById(pharmacyId);

            var drugDtos = _mapper.Map<List<DrugDto>>(pharmacy.Drugs);
            return drugDtos;
                
        }

        

        public DrugDto GetById(int pharmacyId, int drugId)
        {
            var pharmacy = GetPharmacyById(pharmacyId);
            var drug = _dbContext
                .Drugs
                .FirstOrDefault(d => d.Id == drugId);

            if(drug is null || drug.PharmacyId != pharmacyId)
            {
                throw new NotFoundException($"Drug with id: {drugId} not exist");
            }

            return _mapper.Map<DrugDto>(drug);
                
        }

        public int Create(int pharmacyId, CreateDrugDto dto)
        {
            var pharmacy = GetPharmacyById(pharmacyId);
            var drug = _mapper.Map<Drug>(dto);
            drug.PharmacyId = pharmacyId;
            _dbContext.Drugs.Add(drug);
            _dbContext.SaveChanges();
            return drug.Id;
        }

        public void DeletedById(int pharmacyId, int drugId)
        {
            _logger.LogWarning($"Attempt to remove drug id: {drugId} from the pharmacy with id: {pharmacyId}");
            var pharmacy = GetPharmacyById(pharmacyId);
            var drug = _dbContext
                .Drugs
                .FirstOrDefault(d => d.Id == drugId);
            if(drug is null || drug.PharmacyId != pharmacyId)
            {
                throw new NotFoundException($"Drug with id: {drugId} with pharmacy Id {pharmacyId} not exist");
            }
            _dbContext.Remove(drug);
            _dbContext.SaveChanges();
        }

        public void DeletedAllDrugsPharmacyWithId(int pharmacyId)
        {
            _logger.LogWarning($"Attempt to remove all drugs from the pharmacy with id: {pharmacyId}");
            var pharmacy = GetPharmacyById(pharmacyId);
            var drugs = _dbContext.Drugs;
            if(drugs is null)
            {
                throw new NotFoundException($"This pharmacy with id: {pharmacyId} not have any drugs");
            }
            _dbContext.RemoveRange(drugs);
            _dbContext.SaveChanges();
        }

        
        public IEnumerable<DrugDto> GetAllByNameOfSubstance(int pharmacyId, string nameOfSubstance)
        {
            var pharmacy = GetPharmacyById(pharmacyId);
            
            var drugs = _dbContext.Drugs.Where(d => d.SubstancesName == nameOfSubstance);
            if(drugs.Count() == 0)
            {
                throw new NotFoundException($"Drugs with this substances: {nameOfSubstance} not found in this Pharmacy");
            }
            var drugDtos = _mapper.Map<List<DrugDto>>(drugs);
            return drugDtos;
        }
        

        private Pharmacy GetPharmacyById(int pharmacyId)
        {
            var pharmacy = _dbContext
                .Pharmacies
                .Include(p => p.Drugs)
                .FirstOrDefault(p => p.Id == pharmacyId);

            if(pharmacy is null)
            {
                throw new NotFoundException($"Pharmacy with id: {pharmacyId} not exist");
            }
            return pharmacy;
        }
    }
}
