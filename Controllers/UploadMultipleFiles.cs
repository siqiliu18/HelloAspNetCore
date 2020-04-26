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
    public class UploadMultipleFilesController : ControllerBase
    {
        [HttpPost("UploadMultipleFiles")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {

            long size = files.Sum(f => f.Length);

            // Full path to file in temp location
            //var filePath = Path.GetTempFileName();
            var dataDir = Directory.GetCurrentDirectory() + "/Data/";
            Directory.CreateDirectory(dataDir);

            foreach (var formFile in files)
            {
                var filePath = dataDir + formFile.FileName;
                if (formFile.Length > 0)
                    using (var stream = new FileStream(filePath, FileMode.Create))
                        await formFile.CopyToAsync(stream);
            }

            // Process uploaded files

            return Ok(new { count = files.Count, path = dataDir});
        }
    }
}