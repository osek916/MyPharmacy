﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MyPharmacy.Entities;
using MyPharmacy.Models;
using MyPharmacy.Models.OrderForPharmacyDtos;
using MyPharmacy.Models.Queries;
using MyPharmacy.Services;
using System;

namespace MyPharmacy.Controllers
{
    [Route("api/orderforpharmacy")]
    [ApiController]
    public class OrderForPharmacyController : ControllerBase
    {
        private readonly IOrderForPharmacyService _orderForPharmacyService;
        public OrderForPharmacyController(IOrderForPharmacyService orderForPharmacyService)
        {
            _orderForPharmacyService = orderForPharmacyService;
        }
      
        [HttpGet("{id}")]
        [Authorize(Roles = "Manager, Pharmacist")]
        public ActionResult<OrderForPharmacyDto> GetOneById([FromRoute] int id)
        {
            var orderForPharmacyDtos = _orderForPharmacyService.GetOneById(id);
            return orderForPharmacyDtos;
        }

        [HttpGet]
        [Authorize(Roles = "Manager, Pharmacist")]
        public ActionResult<PagedResult<OrderForPharmacyDto>> GetAll([FromQuery] OrderForPharmacyGetAllQuery query)
        {
            var orderForPharmaciesDtos = _orderForPharmacyService.GetAll(query);
            return orderForPharmaciesDtos;
        }
  
        [HttpPost]
        [Authorize(Roles = "Manager, Pharmacist")]
        public ActionResult CreateOrderForPharmacy([FromBody] CreateOrderForPharmacyDto dto)
        {
            var id = _orderForPharmacyService.CreateOrderForPharmacy(dto);
            return Created($"api/orderforpharmacy/{id}", null);
        }

        [HttpPatch("{id}")]
        public ActionResult UpdateByPatch([FromBody] JsonPatchDocument<OrderForPharmacy> orderForPharmacyPatchModel, [FromRoute] int id)
        {
            _orderForPharmacyService.UpdateByPatch(orderForPharmacyPatchModel, id);
            return Ok();
        }

        [HttpPut("status/{id}")]
        [Authorize(Roles = "Manager, Pharmacist")]
        public ActionResult UpdateStatusOfOrder([FromRoute] int id,  [FromBody] string status)
        {
            _orderForPharmacyService.UpdateStatusOfOrder(id, status);
            return Ok();
        }

        [HttpPut("dateofreceipt/{id}")]
        [Authorize(Roles = "Manager, Pharmacist")]
        public ActionResult UpdateDateOfReceiptOfOrder([FromRoute] int id, [FromBody]DateTime? dt)
        {
            _orderForPharmacyService.UpdateDateOfReceiptOfOrder(id, dt);
            return Ok();
        }

        [HttpPut("drug/{id}")]
        [Authorize(Roles = "Manager, Pharmacist")]
        public ActionResult AddDrugToOrderForPharmacy([FromRoute] int id, [FromBody] AddDrugToOrderDto dto)
        {
            _orderForPharmacyService.AddDrugToOrder(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteById([FromRoute] int id)
        {
            _orderForPharmacyService.DeleteById(id);
            return NoContent();
        }
        
    }
}
