using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPharmacy.Models;
using MyPharmacy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Controllers
{
    [Route("api/pharmacy/{pharmacyId}/drug")]
    [ApiController]
    public class DrugController : ControllerBase
    {
        private readonly IDrugService _drugService;
        public DrugController(IDrugService drugService)
        {
            _drugService = drugService;
        }


        [HttpGet]
        [Authorize(Roles = "Admin, Manager, Pharmacist")]
        public ActionResult<PagedResult<DrugDto>> GetAll([FromRoute] int pharmacyId, [FromQuery] DrugGetAllQuery query)
        {
            var drugsDto = _drugService.GetAll(pharmacyId, query);
            return Ok(drugsDto);
        }

        [HttpGet("{drugId}")]
        [Authorize(Roles = "Admin, Manager, Pharmacist")]
        public ActionResult<DrugDto> GetById([FromRoute] int pharmacyId, [FromRoute] int drugId)
        {
            var drugDto = _drugService.GetById(pharmacyId, drugId);
            return drugDto;
        }


        [HttpPost]
        [Authorize(Roles = "Manager")]
        public ActionResult<CreateDrugDto> Create([FromRoute] int pharmacyId, [FromBody] CreateDrugDto dto)
        {
            var newDrugId = _drugService.Create(pharmacyId, dto);
            return Created($"api/pharmacy/{pharmacyId}/drug/{newDrugId}", null);
        }


        [HttpPut()]
        [Authorize(Roles = "Admin, Manager, Pharmacist")]
        public ActionResult UpdateDrugById([FromRoute] int pharmacyId,  [FromBody] UpdateDrugDto dto)
        {
            _drugService.Update(pharmacyId,  dto);
            return Ok();
        }

        
        [HttpDelete("{drugId}")]
        [Authorize(Roles = "Admin, Manager, Pharmacist")]
        public ActionResult DeleteById([FromRoute]int pharmacyId, [FromRoute]int drugId)
        {
            _drugService.DeletedById(pharmacyId, drugId);
            return NoContent();
        }
        

        [HttpDelete]
        [Authorize(Roles = "Admin, Manager, Pharmacist")]
        public ActionResult DeleteAllByPharmacyId([FromRoute]int pharmacyId)
        {
            _drugService.DeletedAllDrugsPharmacyWithId(pharmacyId);
            return NoContent();
        }



        /*
        [HttpGet]
        public ActionResult<IEnumerable<DrugDto>> GetAllByCategory([FromRoute]int pharmacyId, [FromQuery] DrugQuery query)
        {
            var drugsDto = _drugService.GetAllByCategory(pharmacyId, query);
            return Ok(drugsDto);
        }
        
        [HttpGet("substance/{nameOfSubstance}")]
        public ActionResult<IEnumerable<DrugDto>> GetAllByNameOfSubstance([FromRoute]int pharmacyId, [FromRoute]string nameOfSubstance)
        {
            var drugsDto = _drugService.GetAllByNameOfSubstance(pharmacyId, nameOfSubstance);
            return Ok(drugsDto);
        }

        [HttpGet("drugname/{nameOfDrug}")]
        public ActionResult<IEnumerable<DrugDto>> GetAllByNameOfDrug([FromRoute]int pharmacyId, [FromRoute]string nameOfDrug)
        {
            var drugsDto = _drugService.GetAllByNameOfDrug(pharmacyId, nameOfDrug);
            return Ok(drugsDto);
        }
        */
        /*
         [HttpPut("{drugId}")]
         [Authorize(Roles = "Admin, Manager")]
         public ActionResult UpdateDrugById([FromRoute] int pharmacyId, [FromRoute]int drugId, [FromBody]UpdateDrugDto dto)
         {
             _drugService.UpdateDrugById(pharmacyId, drugId, dto);
             return Ok();
         }*/


    }
}
