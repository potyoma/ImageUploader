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
        private readonly string _path;

        public UploadsController(IWebHostEnvironment environment)
        {
            _environment = environment;
            _path = $"{_environment.WebRootPath}/Files";
            FindOrCreateDirectory();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                using (var fileStream = new FileStream(
                    $"{_path}/{uploadedFile.FileName}", FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

                return Ok("You made it!");
            }

            return BadRequest("File is empty");
        }

        private void FindOrCreateDirectory()
        {
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
        }
    }
}