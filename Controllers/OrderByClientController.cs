using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPharmacy.Models;
using MyPharmacy.Models.OrderByClientDtos;
using MyPharmacy.Models.Queries;
using MyPharmacy.Services;

namespace MyPharmacy.Controllers
{
    [Route("api/orderbyclient")]
    [ApiController]
    public class OrderByClientController : ControllerBase
    {
        private readonly IOrderByClientService _orderByClientService;

        public OrderByClientController(IOrderByClientService orderByClientService)
        {
            _orderByClientService = orderByClientService;
        }
        
        [HttpGet("{numberOfOrder}")]
        [Authorize(Roles = "User")]
        public ActionResult<OrderByClientDto> GetOneByNumberOfOrder([FromRoute] int numberOfOrder)
        {
            var orderByClientDto = _orderByClientService.GetOneByNumberOfOrder(numberOfOrder);
            return orderByClientDto;
        }

        /* po id
        [HttpGet("{numberOfOrder}")]
        [Authorize(Roles = "User")]
        public ActionResult<OrderByClientDto> GetOneByNumberOfOrder([FromRoute] int numberOfOrder)
        {
            var orderByClientDto = _orderByClientService.GetOneByNumberOfOrder(numberOfOrder);
            return orderByClientDto;
        }
        */
        [HttpGet()]
        [Authorize(Roles = "User")]
        public ActionResult<PagedResult<OrderByClientDto>> GetAll([FromQuery] OrderByClientGetAllQuery query)
        {
            var orderByClientDto = _orderByClientService.GetAll(query);
            return orderByClientDto;
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult CreateOrderByClient([FromBody] CreateOrderByClientDto dto)
        {
            var id = _orderByClientService.CreateOrderByClient(dto);
            return Created($"api/orderbyclient/{id}", null);
        }

        
    }
}
