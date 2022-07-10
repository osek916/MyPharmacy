using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPharmacy.Entities;
using MyPharmacy.Models;
using MyPharmacy.Services;

namespace MyPharmacy.Controllers
{
    [Route("api/pharmacy")]
    [ApiController]
    public class OrderByClientController : ControllerBase
    {
        private readonly IOrderByClientService _orderByClientService;

        public OrderByClientController(IOrderByClientService orderByClientService)
        {
            _orderByClientService = orderByClientService;
        }
        /*
        [HttpGet("user/{id}")]
        [Authorize(Roles = "User")]
        public ActionResult<OrderByClientDto> GetOneById([FromRoute] int id)
        {
            var orderByClientDto = _orderByClientService.GetOneById(id);
            return orderByClientDto;
        }

        [HttpGet("user")]
        [Authorize(Roles = "User")]
        public ActionResult<PagedResult<OrderByClientDto>> UserGetAll(OrderByClientGetAllQuery query)
        {
            var orderByClientDto = _orderByClientService.GetAll(id);
            return orderByClientDto;
        }

        [HttpGet("")]
        [Authorize(Roles = "Manager, Pharmacist")]
        public ActionResult<O>
        */ 
    }
}
