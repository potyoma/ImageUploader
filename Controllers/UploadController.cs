using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.IO;

namespace TryingWebApi.Controllers
{
    [ApiController]
    [Route("api/uploadscontroller")]
    public class UploadsController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private string _id;

        public UploadsController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                _id = GetImageId().ToString();
                string fileExtension = uploadedFile.FileName.Substring(
                    uploadedFile.FileName.IndexOf('.'));;

                string path = $"/Files/Image{_id}.{fileExtension}";

                using (var fileStream = new FileStream(
                    _environment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

                return Ok("You made it!");
            }

            return BadRequest("File is empty");
        }

        public int GetImageId()
        {
            int id;

            string path = _environment.WebRootPath + "/Files/";
            string[] files = Directory.GetFiles(path);
            id = files.Length + 1;

            return id;
        }
    }
}