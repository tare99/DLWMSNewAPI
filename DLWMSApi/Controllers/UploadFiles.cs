using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using DLWMSApi.Data;
using Microsoft.AspNetCore.Hosting;
using DLWMSApi.Helpers;
using System.Net.Http;

namespace DLWMSApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UploadFiles : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public UploadFiles(ApplicationDbContext context, [FromServices] IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            FileDetail fileDetail = new FileDetail();
            foreach (var file in files)
            {
                var fileType = Path.GetExtension(file.FileName);
                var docName = Path.GetFileName(file.FileName);
                if (fileType.ToLower() == ".pdf" || fileType.ToLower() == ".doc" || fileType.ToLower() == ".rar")
                {
                    var filePath = _env.WebRootPath;

                    fileDetail.Id = Guid.NewGuid();
                    fileDetail.DocumentName = docName;
                    fileDetail.DocType = fileType;
                    fileDetail.DocUrl = Path.Combine(filePath, "Files", fileDetail.Id.ToString() + fileDetail.DocType);

                    using (var stream = new FileStream(fileDetail.DocUrl, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    _context.Add(fileDetail);
                    await _context.SaveChangesAsync();
                }
                else    
                    return BadRequest();  
            }
            return Ok();

        }

        [HttpGet]
        public IActionResult Download(Guid id)
        {
            var fileDetail = _context.FileDetail
            .Where(x => x.Id == id)
            .FirstOrDefault();

            if(fileDetail!=null)
            {
                var path = _env.WebRootPath;
                var fileReadPath = Path.Combine(path, "Files", fileDetail.Id.ToString() + fileDetail.DocType);
                var file = System.IO.File.OpenRead(fileReadPath);
                return File(file, "application/octet-stream", fileDetail.DocumentName);
            }
            else
            {
                return StatusCode(404);
            }
        }

    }
}
