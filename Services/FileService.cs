using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using MyPharmacy.Exceptions;
using MyPharmacy.Models.FileDtos;
using System.IO;

namespace MyPharmacy.Services
{
    public interface IFileService
    {
        public GetFileDto GetDrugLeaflet(string fileName);
        public void AddDrugLeaflet(IFormFile file);
    }
    public class FileService : IFileService
    {
        FileExtensionContentTypeProvider _contentTypeProvider;
        public FileService(FileExtensionContentTypeProvider contentTypeProvider)
        {
            _contentTypeProvider = contentTypeProvider;
        }

        public GetFileDto GetDrugLeaflet(string fileName)
        {
            var rootPath = Directory.GetCurrentDirectory();
            var filePath = $"{rootPath}/MyFiles/DrugLeaflet/{fileName}";
            var fileExists = System.IO.File.Exists(filePath);
            if (!fileExists)
                throw new NotFoundException($"File with this name not exist");

            _contentTypeProvider.TryGetContentType(fileName, out string contentType);
            if (contentType != "application/pdf")
                throw new BadRequestException($"This method only accepts pdf files");

            var fileContents = System.IO.File.ReadAllBytes(filePath);
            var file = new GetFileDto() { ContentType = contentType, FileContents = fileContents, FileName = fileName };
            return file;
        }

        public void AddDrugLeaflet(IFormFile file)
        {
            if (file.Length < 1 || file == null)
            {
                throw new BadRequestException($"File length must be more than 0 or file not exist");
            }

            var rootPath = Directory.GetCurrentDirectory();
            var fileName = file.FileName;
            _contentTypeProvider.TryGetContentType(fileName, out string contentType);
            if (contentType != "application/pdf")
                throw new BadRequestException($"DrugLeaflet must be a pdf file");

            var fullPath = $"{rootPath}/MyFiles/DrugLeaflet/{fileName}";
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }          
        }
    }
}
