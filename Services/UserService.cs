using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyPharmacy.Entities;
using MyPharmacy.Exceptions;
using MyPharmacy.Models;
using MyPharmacy.Models.Enums;
using MyPharmacy.Models.Interfaces;
using MyPharmacy.Models.Queries;
using MyPharmacy.Models.UserDtos;
using System.Collections.Generic;
using System.Linq;

namespace MyPharmacy.Services
{

    public interface IUserService
    {
        public PagedResult<UserDto> GetAll(UserGetAllQuery query);
        public UserDto GetById(int userId);
        public UserDto GetSelfAccount();
        public void DeleteById(int userId);
        public void UpdateByIdWithRole(UpdateUserDtoWithRole dto, int id);
        public void UpdateSelfAccount(UpdateUserDto dto);
        public void UpdatePrivilegesById(UpdateUserRoleAndPharmacyId dto, int id);
        
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
                if (query.GetByChar != '0')
                    users = users.Where(p => p.Pharmacy.Address.City.StartsWith(query.GetByChar.ToString()));

                if (query.SortDirection == SortDirection.ASC)
                    users.OrderBy(u => u.Pharmacy.Address.City);
                else
                    users.OrderByDescending(u => u.Pharmacy.Address.City);
            }
            else
            {
                if (query.GetByChar != '0')
                    users = users.Where(p => p.LastName.StartsWith(query.GetByChar.ToString()));

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

        public void UpdateByIdWithRole(UpdateUserDtoWithRole dto, int id)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
           
            if(user == null)
            {
                throw new NotFoundException($"User with id {id} not found");
            }
            
            user = AssignNewDataToUser(dto, user);
            if (dto.PharmacyId != null)
                user.PharmacyId = dto.PharmacyId;

                user.RoleId = dto.RoleId;
            _dbContext.SaveChanges();
        }

        public void UpdatePrivilegesById(UpdateUserRoleAndPharmacyId dto, int id)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (user is null)
            {
                throw new NotFoundException($"User with id {id} not found");
            }

            if (_userContextService.Role == "Manager")
            {
                if (user.PharmacyId is null)
                {
                    user.PharmacyId = _userContextService.PharmacyId;
                }
                else
                {
                    if (_userContextService.PharmacyId == user.PharmacyId)
                        throw new ForbiddenException($"You dont have a permission to update this user");
                }
            }
            else
            {
                if (dto.PharmacyId != null)
                {
                    user.PharmacyId = dto.PharmacyId;
                }
            }
            user.RoleId = dto.RoleId;
            _dbContext.SaveChanges();
        }

        public void UpdateSelfAccount(UpdateUserDto dto)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == _userContextService.GetUserId);
            if (user == null)
                throw new NotFoundException("Your account was changed during the session");

            user = AssignNewDataToUser(dto, user);
            _dbContext.SaveChanges();
        }
     
        public UserDto GetSelfAccount()
        {
            var user = _dbContext.Users.First(u => u.Id == _userContextService.GetUserId);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
            

        }
        public UserDto GetById(int userId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if(user is null)
            {
                throw new NotFoundException($"User with this Id: {userId} doesn't exist");
            }
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public void DeleteById(int userId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user is null)
            {
                throw new NotFoundException($"User with this Id: {userId} doesn't exist");
            }
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }

        private User AssignNewDataToUser(IUpdateUserDto dto, User user)
        {
            user.Email = dto.Email;
            user.DateOfBirth = dto.DateOfBirth;
            user.FirstName = dto.FirstName;
            user.Gender = dto.Gender;
            user.LastName = dto.LastName;
            user.Nationality = dto.Nationality;

            return user;
        }
    }
}