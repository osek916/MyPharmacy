using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPharmacy.Entities;
using MyPharmacy.Models;
using MyPharmacy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            if (pharmacyDto is null)
            {
                return NotFound();
            }
            return pharmacyDto;
        }



        //Manager tworzy TYLKO JEDNĄ aptekę
        [HttpPost]
        [Authorize(Roles = "Manager")]
        public ActionResult CreatePharmacy([FromBody] CreatePharmacyDto dto)
        {
            var id = _pharmacyService.Create(dto);
            return Created($"api/pharmacy/{id}", null);
        }

        //Manager może edytować stworzoną przez siebie aptekę
        [HttpPut]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Update([FromBody] UpdatePharmacyDto dto)
        {
            _pharmacyService.Update(dto);
            return Ok();
        }

        //ADMIN
        [HttpDelete]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Delete([FromQuery]int bodyId)
        {
            _pharmacyService.Delete(bodyId);
            return NoContent();
        }
  


    }
}
