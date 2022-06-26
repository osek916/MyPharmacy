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
        void Update(UpdateOrderForPharmacyDto dto);
        void UpdateByPatch(JsonPatchDocument orderForPharmacyPatchModel, int id);
        void DeleteById(int id);
        void AddDrugToOrder(List<AddDrugToOrderDto> dto);

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
                .Where(o => o.PharmacyId == _userContextService.PharmacyId && ( query.year == null || o.DateOfOrder.Year == query.year));
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

        public void Update(UpdateOrderForPharmacyDto dto)
        {
            
        }

        public void AddDrugToOrder(List<AddDrugToOrderDto> dto) //zrobić sprawdzanie
        {

        }

        public void UpdateByPatch(JsonPatchDocument orderForPharmacyPatchModel, int id)
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
        //public void UpdateStatus(UpdateOrderForPharmacyDto dto)
        //{
        //    if (_userContextService.PharmacyId == null)
        //    {
        //        throw new ForbiddenException($"You don't have any Pharmacy");
        //    }

        //}
    }
}
