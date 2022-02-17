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
        
        /*
        [HttpGet]
        public ActionResult<IEnumerable<DrugDto>> GetAllByCategory([FromQuery] DrugQuery query)
        {
            //var drugsDto = _pharmacyService.GetAllByCategory(query);
            //return Ok(drugsDto);

            return Ok();
        }
        

        [HttpGet("drugs/{nameOfSubstance}")]
        public ActionResult<IEnumerable<DrugDto>> GetAllByNameOfSubstance([FromRoute]string nameOfSubstance)
        {
            var drugsDto = _pharmacyService.GetAllByNameOfSubstance(nameOfSubstance);
            return Ok(drugsDto);
        }
        */

        //ADMIN
        [HttpDelete]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Delete([FromQuery]int bodyId)
        {
            _pharmacyService.Delete(bodyId);
            return NoContent();
        }

        //Manager może edytować stworzoną przez siebie aptekę
        [HttpPut]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Update([FromBody] UpdatePharmacyDto dto)
        {
            _pharmacyService.Update(dto);
            return Ok();
        }

        

        //Manager tworzy TYLKO JEDNĄ aptekę
        [HttpPost]
        [Authorize(Roles ="Manager")]
        public ActionResult CreatePharmacy([FromBody] CreatePharmacyDto dto)
        {
            var id = _pharmacyService.Create(dto);
            return Created($"api/pharmacy/{id}", null);
        }

        //DO ZROBIENIA
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<PharmacyDto>> GetAll()
        {
            var pharmaciesDtos = _pharmacyService.GetAll();
            return Ok(pharmaciesDtos);
        }

        //PagedResult<SearchEnginePharmacyDto> GetPharmacies(SearchEnginePharmacyQuery query)
        //DO ZROBIENIA
        //ADMIN
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Manager, Pharmacist")]
        public ActionResult<PharmacyDto> GetOne([FromRoute] int id)
        {
            var pharmacyDto = _pharmacyService.GetOne(id);
            
            if(pharmacyDto is null)
            {
                return NotFound();
            }
            return pharmacyDto;
        }


    }
}
