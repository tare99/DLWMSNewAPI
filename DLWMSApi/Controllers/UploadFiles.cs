using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace DLWMSApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UploadFiles : Controller
    {

        [HttpGet]
        public  string[] SendFiles()
        {
            string[] fajlovi = Directory.GetFiles(@"C:\Users\Tarik\source\repos\DLWMSApi\DLWMSApi\Uploads\");
            return fajlovi;
        }

        [HttpPost]
        public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    string filePath = @"C:\Users\Tarik\source\repos\DLWMSApi\DLWMSApi\Uploads\"+formFile.FileName;

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            return Ok(new { count = files.Count, size });
        }

       
    }
}
