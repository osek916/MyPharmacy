﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPharmacy.Models;
using MyPharmacy.Services;

namespace MyPharmacy.Controllers
{
    [Route("api/pharmacy")]
    [ApiController]
    public class PharmacyController : ControllerBase
    {
        private readonly IPharmacyService _pharmacyService;

        public PharmacyController(IPharmacyService pharmacyService)
        {
            _pharmacyService = pharmacyService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Manager, Pharmacist")]
        public ActionResult<PagedResult<PharmacyDto>> GetAll([FromQuery] PharmacyGetAllQuery query)
        {
            var pharmaciesDtos = _pharmacyService.GetAll(query);
            return Ok(pharmaciesDtos);
        }

        
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Manager, Pharmacist")]
        public ActionResult<PharmacyDto> GetOne([FromRoute] int id)
        {
            var pharmacyDto = _pharmacyService.GetOne(id);         
            return pharmacyDto;
        }


        [HttpPost]
        [Authorize(Roles = "Manager")]
        public ActionResult CreatePharmacy([FromBody] CreatePharmacyDto dto)
        {
            var id = _pharmacyService.Create(dto);
            return Created($"api/pharmacy/{id}", null);
        }


        [HttpPut]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Update([FromBody] UpdatePharmacyDto dto)
        {
            _pharmacyService.Update(dto);
            return Ok();
        }
    
        
        [HttpDelete]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Delete([FromQuery]int bodyId)
        {
            _pharmacyService.Delete(bodyId);
            return NoContent();
        }
  


    }
}
