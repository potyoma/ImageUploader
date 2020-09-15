using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.IO;

namespace TryingWebApi.Controllers
{
    [ApiController]
    [Route("api/getImageLink")]
    public class LinkController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public LinkController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpGet("Image{imageId}}")]
        public async Task<IFormFile> GetImage()
        {
            // TODO: Finish method
            // TODO: Make it return image
            
        }
    }
}