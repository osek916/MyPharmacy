using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyPharmacy.Entities;
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
    }

    public class PharmacyService : IPharmacyService
    {
        private readonly PharmacyDbContext _dbContext;
        private readonly IMapper _mapper;

        public PharmacyService(PharmacyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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
