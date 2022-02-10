﻿using AutoMapper;
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
    public interface IPharmacyService
    {
        IEnumerable<PharmacyDto> GetAll();
        PharmacyDto GetOne(int id);
        int Create(CreatePharmacyDto dto);
        void Update(UpdatePharmacyDto dto, int id);
        void Delete(int id);
        IEnumerable<DrugDto> GetAllByNameOfSubstance(string nameOfSubstance);
    }

    public class PharmacyService : IPharmacyService
    {
        private readonly PharmacyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public PharmacyService(PharmacyDbContext dbContext, IMapper mapper, ILogger<PharmacyService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public IEnumerable<DrugDto> GetAllByNameOfSubstance(string nameOfSubstance)
        {
            var drugs = _dbContext.Drugs.Where(d => d.SubstancesName == nameOfSubstance);
            if(drugs.Count() == 0)
            {
                throw new NotFoundException($"Drugs with name of substances: {nameOfSubstance} not exist");
            }
            var drugsDto = _mapper.Map<List<DrugDto>>(drugs);
            return drugsDto;
        }
        public void Delete(int id)
        {
            _logger.LogWarning($"Attempt to remove pharmacy id: {id}");
            var pharmacy = _dbContext
                .Pharmacies
                .FirstOrDefault(p => p.Id == id);
            if(pharmacy is null)
            {
                //_logger.LogWarning
                throw new NotFoundException($"Pharmacy with id: {id} not Found");
            }
            _dbContext.Pharmacies.Remove(pharmacy);
            _dbContext.SaveChanges();
        }

        public void Update(UpdatePharmacyDto dto, int id)
        {
            var pharmacy = _dbContext
                .Pharmacies
                .Include(x => x.Address)
                .FirstOrDefault(p => p.Id == id);

            if(pharmacy is null)
            {
                throw new NotFoundException($"Pharmacy with {id} not found");
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

        public int Create(CreatePharmacyDto dto)
        {
            var pharmacy = _mapper.Map<Pharmacy>(dto);
            _dbContext.Add(pharmacy);
            _dbContext.SaveChanges();

            return pharmacy.Id;
        }

        public PharmacyDto GetOne(int id)
        {
            var pharmacy = _dbContext
                .Pharmacies
                .Include(x => x.Address)
                .Where(x => x.Id == id)
                .FirstOrDefault();
            //.Include(x => x.Drugs)

            if(pharmacy is null)
            {
                throw new NotFoundException($"Pharmacy with {id} not found");
            }

            var pharmacyDto = _mapper.Map<PharmacyDto>(pharmacy);
            return pharmacyDto;
        }

        public IEnumerable<PharmacyDto> GetAll()
        {
            var pharmacies = _dbContext
                .Pharmacies
                .Include(x => x.Address)
                .Include(x => x.Drugs)
                .ToList();

            var pharmaciesDto = _mapper.Map<List<PharmacyDto>>(pharmacies);
            return pharmaciesDto;
        }
    }
}
