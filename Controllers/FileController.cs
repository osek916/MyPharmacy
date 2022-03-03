using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPharmacy.Services;

namespace MyPharmacy.Controllers
{
    [Route("file")]
    //[Authorize]
    public class FileController : ControllerBase
    {
        IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet]
        public ActionResult GetDrugLeaflet([FromQuery] string fileName)
        {
            var file = _fileService.GetDrugLeaflet(fileName);
            
            return File(file.FileContents, file.ContentType, file.FileName);
        }

        
        [HttpPost]
        public ActionResult AddDrugLeaflet([FromForm]IFormFile file)
        {
            _fileService.AddDrugLeaflet(file);

            return Ok();      
        }
        
    }
}


