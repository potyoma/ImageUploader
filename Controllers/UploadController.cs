using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.IO;

namespace TryingWebApi.Controllers
{
    [ApiController]
    [Route("api/uploads")]
    public class UploadsController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public UploadsController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                string path =
                    $"{_environment.WebRootPath}/Files/{uploadedFile.FileName}";

                using (var fileStream = new FileStream(
                    path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
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
    }
}