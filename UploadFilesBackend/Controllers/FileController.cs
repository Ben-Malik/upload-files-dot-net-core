using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UploadFilesBackend.Models;
using UploadFilesBackend.Services;


namespace UploadFilesBackend.Controllers
{
    [ApiController]
    public class FileController : Controller
    {
        private readonly FileContext context;

        private readonly FileManager fileManager;

        public FileController(FileContext context)
        {
            this.context = context;
            fileManager = new FileManager(context);
        }

        [HttpGet]
        [Route("view-files")]
        [EnableCors("CorsPolicy")]
        public List<FileModel> displayFiles()
        {
            return fileManager.getFiles();
        }

        [HttpPost]
        [Route("upload")]
        [EnableCors("CorsPolicy")]
        public Task<IActionResult> UploadToFileSystemAsync(IFormFile file)
        {

            fileManager.UploadFileToDB(file);
            
            return ViewBag;
        }

        [HttpPost]
        [Route("save")]
        public ActionResult<FileModel> Save(FileModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!context.isFileModelValid(model))
            {
                ModelState.AddModelError("File Model", "Invalid file model");
                return BadRequest(ModelState);
            }
            context.Files.Add(model);
            context.SaveChanges();

            return model;
        }

    }
}
