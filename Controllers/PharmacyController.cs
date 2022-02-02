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
        public ActionResult<PharmacyDto> GetAll()
        {
            var pharmacyDtos = _pharmacyService.GetAll();
            return Ok(pharmacyDtos);
        }
    }
}
