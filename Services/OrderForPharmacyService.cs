using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using MyPharmacy.Entities;
using MyPharmacy.Exceptions;
using MyPharmacy.Helpers;
using MyPharmacy.Models;
using MyPharmacy.Models.OrderForPharmacyDtos;
using MyPharmacy.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyPharmacy.Services
{
    public interface IOrderForPharmacyService
    {
        OrderForPharmacyDto GetOneById(int id);
        PagedResult<OrderForPharmacyDto> GetAll(OrderForPharmacyGetAllQuery query);
        int CreateOrderForPharmacy(CreateOrderForPharmacyDto dto);
        //void Update(UpdateOrderForPharmacyDto dto);
        void UpdateByPatch(JsonPatchDocument<OrderForPharmacy> orderForPharmacyPatchModel, int id);
        void DeleteById(int id);
        void AddDrugToOrder(int id, AddDrugToOrderDto dto);
        void UpdateStatusOfOrder(int id, string status);
        void UpdateDateOfReceiptOfOrder(int id, DateTime? dateOfReceipt);

    }
    public class OrderForPharmacyService : IOrderForPharmacyService
    {
        private readonly PharmacyDbContext _dbContext;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;
        public OrderForPharmacyService(PharmacyDbContext dbContext, IUserContextService userContextService, IMapper mapper)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
            _mapper = mapper;
        }
        public OrderForPharmacyDto GetOneById(int id)
        {
            var orderForPharmacy = _dbContext
                .OrderForPharmacies
                .Include(o => o.Drugs)
                .AsNoTracking()
                .FirstOrDefault();

            if (orderForPharmacy != null)
                throw new NotFoundException($"Order with id: {id} not found");

            if (orderForPharmacy.PharmacyId != _userContextService.PharmacyId)
                throw new ForbiddenException($"This order doesn't belong to your pharmacy");

            var orderForPharmacyDto = _mapper.Map<OrderForPharmacyDto>(orderForPharmacy);
            return orderForPharmacyDto;
        }

        public PagedResult<OrderForPharmacyDto> GetAll(OrderForPharmacyGetAllQuery query)
        {
            var ordersForPharmacy = _dbContext
                .OrderForPharmacies
                .Include(o => o.Drugs)
                .Include(o => o.Status)
                .Include(o => o.User)
                .Where(o => o.PharmacyId == _userContextService.PharmacyId && ( query.year == null || o.DateOfOrder.Year == query.year))
                .AsNoTracking();
                //.Where(d => query.Phrase == null || (d.Address.City.ToLower().Contains(query.Phrase.ToLower()) || d.Name.ToLower().Contains(query.Phrase.ToLower())));

            var selector = new Dictionary<string, Expression<Func<OrderForPharmacy, object>>>
            {
                {nameof(OrderForPharmacy.DateOfOrder), p => p.DateOfOrder},
                {nameof(OrderForPharmacy.DateOfReceipt), p => p.DateOfReceipt },
                {nameof(OrderForPharmacy.Price), p => p.Price },
                {nameof(OrderForPharmacy.Status.Name), p => p.Status.Name },
                {nameof(OrderForPharmacy.User.Id), p => p.User.Id }
            };

            if (query.SortDirection == SortDirection.ASC)
                ordersForPharmacy = ordersForPharmacy.OrderBy(selector[query.SortBy]);

            else
                ordersForPharmacy = ordersForPharmacy.OrderByDescending(selector[query.SortBy]);

            var finalOrdersForPharmacy = PaginationHelper<OrderForPharmacy, OrderForPharmacyGetAllQuery>.ReturnPaginatedList(query, ordersForPharmacy);
            var finalOrdersForPharmacyDtos = _mapper.Map<List<OrderForPharmacyDto>>(finalOrdersForPharmacy);
            var result = new PagedResult<OrderForPharmacyDto>(finalOrdersForPharmacyDtos, finalOrdersForPharmacyDtos.Count(), query.PageSize, query.PageNumber);

            return result;
        }

        public int CreateOrderForPharmacy(CreateOrderForPharmacyDto dto)
        {
            if (_userContextService.PharmacyId == null)
            {
                throw new ForbiddenException($"You don't have any Pharmacy");
            }
            var orderForPharmacy = _mapper.Map<OrderForPharmacy>(dto);
            var status = _dbContext
                .Statuses
                .First(s => s.Name == dto.StatusName); //do walidatora sprawdzanie 

            orderForPharmacy.PharmacyId = (int)_userContextService.PharmacyId;
            //orderForPharmacy.StatusId = status.Id;
            orderForPharmacy.Status = status;
            orderForPharmacy.DateOfOrder = DateTime.Now;
            orderForPharmacy.OrderDescription = dto.OrderDescription;
            var drugs = _mapper.Map<List<Drug>>(dto.DrugsDtos);
            orderForPharmacy.Drugs = drugs;
            orderForPharmacy.Price = dto.AdditionalCosts + drugs.Sum(x => x.Price);

            _dbContext.OrderForPharmacies.Add(orderForPharmacy);
            _dbContext.SaveChanges();

            return orderForPharmacy.Id;
        }
      
        public void UpdateStatusOfOrder(int id, string status)
        {
            var order = _dbContext.OrderForPharmacies.FirstOrDefault(o => o.Id == id
            && o.PharmacyId == _userContextService.PharmacyId);

            if(order != null)
            {
                var dbStatus = _dbContext.Statuses.AsNoTracking()
                    .FirstOrDefault(s => s.Name.ToLower() == status.ToLower());

                if(dbStatus != null)
                {
                    order.StatusId = dbStatus.Id;
                    _dbContext.SaveChanges();
                }
                else
                {
                    throw new BadRequestException($"The given status is invalid");
                }
            }
            else
            {
                throw new NotFoundException($"Order not found");
            }
        }

        public void UpdateDateOfReceiptOfOrder(int id, DateTime? dateOfReceipt)
        {
            var order = _dbContext.OrderForPharmacies.FirstOrDefault(o => o.Id == id
            && o.PharmacyId == _userContextService.PharmacyId);

            if (order != null)
            {
                //if (dateOfReceipt == null)
                if(!dateOfReceipt.HasValue)
                {
                    order.DateOfReceipt = DateTime.Now;
                }
                else
                {
                    if(order.DateOfOrder <= dateOfReceipt)
                    {
                        order.DateOfReceipt = dateOfReceipt;
                    }
                    else
                    {
                        throw new BadRequestException($"Date of order must be less than date of receipt");
                    }                    
                }
                _dbContext.SaveChanges();
            }
            else
            {
                throw new NotFoundException($"Order not found");
            }
        }

        public void AddDrugToOrder(int id, AddDrugToOrderDto dto) //zrobić sprawdzanie
        {
            var order = _dbContext.OrderForPharmacies
                .Include(d => d.Drugs)
                .ThenInclude(d => d.DrugInformation)
                .FirstOrDefault(o => o.Id == id
                && o.PharmacyId == _userContextService.PharmacyId);

            if(order != null)
            {

                var drug = order.Drugs
                    .FirstOrDefault(d => d.DrugInformation.DrugsName == dto.DrugsName 
                    && d.DrugInformation.SubstancesName == dto.SubstancesName
                    && d.DrugInformation.NumberOfTablets == dto.NumberOfTablets 
                    && d.DrugInformation.MilligramsPerTablets == dto.MilligramsPerTablets);

                if (drug != null)
                {
                    //Także odejmuje
                    if(drug.AmountOfPackages - dto.AmountOfPackages < 0)
                    {
                        drug.AmountOfPackages += dto.AmountOfPackages;
                        drug.Price += (dto.Price * dto.AmountOfPackages) + dto.AdditionalCosts;
                        _dbContext.SaveChanges();
                    }
                    else
                    {
                        throw new BadRequestException($"The number of packages after the substraction must be equal or greater than 0");
                    }
                }
                else
                {                   
                    var drugInformation = _dbContext.DrugInformations.FirstOrDefault(d => d.DrugsName == dto.DrugsName
                    && d.SubstancesName == dto.SubstancesName
                    && d.NumberOfTablets == dto.NumberOfTablets
                    && d.MilligramsPerTablets == dto.MilligramsPerTablets);

                    if(drugInformation != null)
                    {
                        order.Drugs.Add(new Drug()
                        {
                            Price = dto.Price,
                            AmountOfPackages = dto.AmountOfPackages,
                            PharmacyId = (int)_userContextService.PharmacyId,
                            DrugInformation = drugInformation
                        });
                    }
                    else
                    {
                        throw new NotFoundException($"DrugInformation not found");
                    }
                    
                }
            }
            else
            {
                throw new NotFoundException($"Order not found");
            }
        }

        public void UpdateByPatch(JsonPatchDocument<OrderForPharmacy> orderForPharmacyPatchModel, int id)
        {
            UserHasAPharmacy();

            var order = _dbContext.OrderForPharmacies.FirstOrDefault(o => o.PharmacyId == _userContextService.PharmacyId && o.Id == id);
            if(order != null)
            {
                orderForPharmacyPatchModel.ApplyTo(order);
                _dbContext.SaveChanges();
            }
            else
                throw new NotFoundException("Order with this id doesn't exist in your pharmacy orders");  
        }

        public void DeleteById(int id)
        {
            UserHasAPharmacy();
            var order = _dbContext.OrderForPharmacies.FirstOrDefault(o => o.PharmacyId == _userContextService.PharmacyId && o.Id == id);
            if(order != null)
            {
                _dbContext.OrderForPharmacies.Remove(order);
                _dbContext.SaveChanges();
            }
            else
                throw new NotFoundException("Order with this id doesn't exist in your pharmacy orders");
        }

        public void UserHasAPharmacy()
        {
            if (_userContextService.PharmacyId == null)
                throw new ForbiddenException($"You don't have any Pharmacy");
        }
    }
}
