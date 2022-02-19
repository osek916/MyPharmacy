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
    [Route("api/drugcategory")]
    [ApiController]
    public class DrugCategoryController : ControllerBase
    {
        private readonly IDrugCategoryService _drugCategoryService;

        public DrugCategoryController(IDrugCategoryService drugCategoryService)
        {
            _drugCategoryService = drugCategoryService;
        }

        [HttpGet]
        public ActionResult<PagedResult<DrugCategoryDto>> GetAll([FromQuery] DrugCategoryGetAllQuery query)
        {
            var drugCategoriesDto = _drugCategoryService.GetAll(query);
            return drugCategoriesDto;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([FromBody] CreateDrugCategoryDto dto)
        {
            var newDrugCategoryId = _drugCategoryService.Create(dto);
            return Created($"api/drugcategory/{newDrugCategoryId}", null);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteById(int id)
        {
            _drugCategoryService.DeleteById(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult Update([FromQuery] UpdateDrugCategoryDto dto, int id)
        {
            _drugCategoryService.UpdateById(dto, id);
            return Ok();
        }
    }
}
