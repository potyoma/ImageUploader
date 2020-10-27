using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Collections.Generic;
using TryingWebApi.Models;

namespace TryingWebApi.Controllers
{
    [ApiController]
    [Route("/api/get/")]
    public class DownloadController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly Dictionary<string, string> _mimeTypes =
            new Dictionary<string, string>
            {
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".png", "image/png"},
                {".svg", "image/svg+xml"}
            };

        public DownloadController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpGet("{fileName}")]
        public async Task<IActionResult> Download(string fileName)
        {
            string path = $"{_environment.WebRootPath}/Files/{fileName}";

            if (System.IO.File.Exists(path))
            {
                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                string ext = Path.GetExtension(path);
                System.IO.File.Delete(path);
                return File(memory, _mimeTypes[ext], fileName);
            }

            return BadRequest("No such file!");
        }
    }
}