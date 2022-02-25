using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using MyPharmacy.Exceptions;
using MyPharmacy.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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


/*
 * [HttpGet]
        public ActionResult GetDrugLeaflet([FromQuery] string fileName)
        {
            var rootPath = Directory.GetCurrentDirectory();
            var filePath = $"{rootPath}/MyFiles/DrugLeaflet/{fileName}";
            var fileExists = System.IO.File.Exists(filePath);
            if(!fileExists)     
                return NotFound($"File with this name not exist");
            
            //var contentTypeProvider = new FileExtensionContentTypeProvider();
            _contentTypeProvider.TryGetContentType(fileName, out string contentType);
            if (contentType != "application/pdf")
                throw new BadRequestException($"This method only accepts pdf files");
            
            var fileContents = System.IO.File.ReadAllBytes(filePath);
            return File(fileContents, contentType, fileName);
        }

        [HttpPost]
        public ActionResult AddDrugLeaflet([FromForm]IFormFile file)
        {

            if(file.Length > 0 && file != null)
            {
                var rootPath = Directory.GetCurrentDirectory();
                var fileName = file.FileName;
                _contentTypeProvider.TryGetContentType(fileName, out string contentType);
                if (contentType != "application/pdf")
                    throw new BadRequestException($"DrugLeaflet must be a pdf file");

                var fullPath = $"{rootPath}/MyFiles/DrugLeaflet/{fileName}";
                using(var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return Ok();
            }
            return BadRequest();
        }
*/