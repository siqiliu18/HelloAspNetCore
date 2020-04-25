using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace HelloAspNetCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadSingleFileController : ControllerBase
    {
        [HttpPost("UploadSingleFile")]
        public async Task<IActionResult> Post(IFormFile file)
        {

            // Full path to file in temp location
            //var filePath = Path.GetTempFileName();
            var dataDir = Directory.GetCurrentDirectory() + "/Data/";
            Directory.CreateDirectory(dataDir);
            var filePath = dataDir + file.FileName;

            if (file.Length > 0)
                using (var stream = new FileStream(filePath, FileMode.Create))
                    await file.CopyToAsync(stream);

            // Process uploaded files

            return Ok(new { count = 1, path = filePath});
        }
    }
}