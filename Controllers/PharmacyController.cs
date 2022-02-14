using Microsoft.AspNetCore.Mvc;
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

        [HttpDelete("{id}")]
        public ActionResult DeleteById([FromRoute] int id)
        {
            _pharmacyService.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdatePharmacyDto dto, [FromRoute] int id )
        {
             _pharmacyService.Update(dto, id);

            return Ok();
        }

        [HttpPost]
        public ActionResult CreatePharmacy([FromBody] CreatePharmacyDto dto)
        {
            var id = _pharmacyService.Create(dto);

            return Created($"api/pharmacy/{id}", null);
        }


        [HttpGet]
        public ActionResult<IEnumerable<PharmacyDto>> GetAll()
        {
            var pharmaciesDtos = _pharmacyService.GetAll();
            return Ok(pharmaciesDtos);
        }

        [HttpGet("{id}")]
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
