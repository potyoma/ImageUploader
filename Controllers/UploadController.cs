using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.IO;
using TryingWebApi.Models;

namespace TryingWebApi.Controllers
{
    [ApiController]
    [Route("api/uploadscontroller")]
    public class UploadsController : ControllerBase
    {
        private readonly FilesContext _db;
        private readonly IWebHostEnvironment _environment;

        public UploadsController(IWebHostEnvironment environment,
            FilesContext db)
        {
            _environment = environment;
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                string path =
                    $"{_environment.WebRootPath}/Files/{uploadedFile.FileName}";

                _db.Files.Add(new FileModel
                {
                    Id = SetId(_environment.WebRootPath),
                    Name = uploadedFile.FileName,
                    Path = path
                });

                using (var fileStream = new FileStream(
                    path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                    await _db.SaveChangesAsync();
                }

                return Ok("You made it!");
            }

            return BadRequest("File is empty");
        }

        public int SetId(string pathToUploads)
        {
            int id;
            int countFiles = Directory.GetFiles(pathToUploads).Length;

            if (countFiles > 20)
            {
                return 1;
            }

            id = Directory.GetFiles(pathToUploads).Length + 1;
            return id;
        }

        public bool DeleteExtraFile(string pathToUploads) 
        {
            // TODO: Find file by id
            // TODO: Remove it
            // TODO: Remove field in database
            return false;
        }
    }
}