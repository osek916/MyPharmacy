using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyPharmacy.Entities;
using MyPharmacy.Models;
using MyPharmacy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPharmacy.Controllers
{
    [Route("api/druginformation")]
    [ApiController]
    public class DrugInformationController : ControllerBase
    {
       
        private readonly IDrugInformationService _drugInformationService; 

        public DrugInformationController(IDrugInformationService drugInformationService)
        {
            _drugInformationService = drugInformationService;
        }

        [HttpGet]
        public ActionResult<PagedResult<DrugInformationDto>> GetAll([FromQuery] GetAllDrugInformationQuery query)
        {
            var drugInformations = _drugInformationService.GetAll(query);
            return drugInformations;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([FromBody] CreateDrugInformationDto dto)
        {
            var newDrugInformationId = _drugInformationService.Create(dto);
            return Created($"api/druginformation/{newDrugInformationId}", null);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteById(int id)
        {
            _drugInformationService.DeleteById(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult Update([FromQuery] UpdateDrugInformationDto dto, int id)
        {
            _drugInformationService.UpdateById(dto, id);
            return Ok();
        }
    }
}
