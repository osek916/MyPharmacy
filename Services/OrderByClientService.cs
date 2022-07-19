using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyPharmacy.Entities;
using MyPharmacy.Exceptions;
using MyPharmacy.Helpers;
using MyPharmacy.Models;
using MyPharmacy.Models.OrderByClientDtos;
using MyPharmacy.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyPharmacy.Services
{

    public interface IOrderByClientService
    {
        OrderByClientDto GetOneByNumberOfOrder(int NumberOfOrder);
        PagedResult<OrderByClientDto> GetAll(OrderByClientGetAllQuery query);
        //int CreateOrderByClient(CreateOrderByClientDto dto);
    }
    public class OrderByClientService : IOrderByClientService
    {
        private readonly PharmacyDbContext _dbContext;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;
        public OrderByClientService(PharmacyDbContext dbContext, IUserContextService userContextService, IMapper mapper)
        {
            _dbContext = dbContext;
            _userContextService = userContextService;
            _mapper = mapper;
        }

        public OrderByClientDto GetOneByNumberOfOrder(int NumberOfOrder)
        {
            var orderByClient = _dbContext
                .OrderByClients
                .Include(o => o.Drugs)
                .AsNoTracking()
                .FirstOrDefault(o => o.NumberOfOrder == NumberOfOrder);

            if (orderByClient.Id == _userContextService.GetUserId)
                throw new ForbiddenException($"This order doesn't belong to you");

            if (orderByClient is null)
                throw new NotFoundException($"Order with number of order: {NumberOfOrder} not found");

            var orderByClientDto = _mapper.Map<OrderByClientDto>(orderByClient);
            return orderByClientDto;
        }

        public PagedResult<OrderByClientDto> GetAll(OrderByClientGetAllQuery query)
        {
            var ordersByClient = _dbContext
                .OrderByClients
                .Include(o => o.Address)
                .Include(o => o.Drugs)
                .ThenInclude(d => d.DrugInformation)
                .Include(o => o.Status)
                .Where(o => o.UserId == _userContextService.GetUserId)
                .AsNoTracking();

            var selector = new Dictionary<string, Expression<Func<OrderByClient, object>>>
            {
                {nameof(OrderForPharmacy.DateOfOrder), o => o.DateOfOrder},
                {nameof(OrderForPharmacy.DateOfReceipt), o => o.DateOfReceipt},
                {nameof(OrderForPharmacy.Status.Name), o => o.Status.Name },
                {nameof(OrderForPharmacy.Price), o => o.Price},
                {nameof(OrderForPharmacy.UserId), o => o.UserId }
            };

            if (query.SortDirection == SortDirection.ASC)
                ordersByClient = ordersByClient.OrderBy(selector[query.SortBy]);

            else
                ordersByClient = ordersByClient.OrderByDescending(selector[query.SortBy]);

            var finalOrdersByClient = PaginationHelper<OrderByClient, OrderByClientGetAllQuery>.ReturnPaginatedList(query, ordersByClient);
            var finalOrdersByClientDtos = _mapper.Map<List<OrderByClientDto>>(finalOrdersByClient);
            var result = new PagedResult<OrderByClientDto>(finalOrdersByClientDtos, finalOrdersByClientDtos.Count(), query.PageSize, query.PageNumber);

            return result;
        }

        
    }
}
