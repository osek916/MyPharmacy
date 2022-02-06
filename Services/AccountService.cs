using MyPharmacy.Entities;
using MyPharmacy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Services
{
    public interface IAccountService
    {
        void RegisterUser(UserRegisterDto dto);
    }
    public class AccountService : IAccountService
    {
        private readonly PharmacyDbContext _dbContext;
        public AccountService(PharmacyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void RegisterUser(UserRegisterDto dto)
        {
            var user = new User()
            {
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                RoleId = dto.RoleId
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
    }
}
