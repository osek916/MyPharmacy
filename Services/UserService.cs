using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPharmacy.Entities;
using MyPharmacy.Exceptions;
using MyPharmacy.Models;
using MyPharmacy.Models.Enums;
using MyPharmacy.Models.Queries;
using MyPharmacy.Models.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Services
{

    public interface IUserService
    {
        public PagedResult<UserDto> GetAll(UserGetAllQuery query);
        public UserDto GetById(int userId);
        public UserDto GetSelfAccount();
        public void UpdateById(UpdateUserDto dto, int userId);
        public void DeleteById(int id);

    }

    public class UserService : IUserService
    {
        private readonly PharmacyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IUserContextService _userContextService;

        public UserService(PharmacyDbContext dbContext, IMapper mapper, ILogger<UserService> logger, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _userContextService = userContextService;
        }

        public PagedResult<UserDto> GetAll(UserGetAllQuery query)
        {
            IQueryable<User> users = _dbContext
                .Users
                .Include(u => u.Pharmacy)
                   .ThenInclude(u => u.Address);


            if (_userContextService.Role != "Admin")
            {
                users = users.Where(u => u.PharmacyId == _userContextService.PharmacyId);
            }   
            
            if (users is null)
                throw new NotFoundException($"Users with this search parameters not found");


            if (query.UserSortBy == UserSortBy.City)
            {
                if (query.SortDirection == SortDirection.ASC)
                    users.OrderBy(u => u.Pharmacy.Address.City);
                else
                    users.OrderByDescending(u => u.Pharmacy.Address.City);
            }
            else
            {
                if (query.SortDirection == SortDirection.ASC)
                    users.OrderBy(u => u.LastName);
                else
                    users.OrderByDescending(u => u.LastName);
            }

            var finalUsers = users
               .Skip((query.PageNumber - 1) * query.PageSize)
               .Take(query.PageSize).ToList();

            var totalItemsCount = users.Count();
            var usersDtos = _mapper.Map<List<UserDto>>(finalUsers);

            var result = new PagedResult<UserDto>(usersDtos, totalItemsCount, query.PageSize, query.PageNumber);
            return result;
        }

        public UpdateByAdmin


        public UserDto GetById(int userId)
        {

        }

        public UserDto GetSelfAccount()
        {

        }


        public void UpdateById(UpdateUserDto dto, int userId)
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

            if (drugCategory is null)
            {
                throw new NotFoundException($"DrugCategory with id: {id} not found");
            }
            _dbContext.DrugCategories.Remove(drugCategory);
            _dbContext.SaveChanges();
        }

    }

}