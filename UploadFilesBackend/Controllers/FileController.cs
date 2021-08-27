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
        public async Task<IActionResult> UploadToFileSystemAsync(IFormFile file)
        {

            var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\Files\\");
            bool basePathExists = System.IO.Directory.Exists(basePath);
            if (!basePathExists) Directory.CreateDirectory(basePath);
            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(basePath, file.FileName);
            var fileSize = file.Length;
            var extension = Path.GetExtension(file.FileName);
            if (!System.IO.File.Exists(filePath))
            {

                var fileModel = new FileModel
                {
                    CreatedAt = DateTime.UtcNow,
                    FileType = file.ContentType,
                    FileName = fileName,
                    FileSize = fileSize
                };

                using (var target = new MemoryStream())
                {
                    await file.CopyToAsync(target);
                    fileModel.FileContent = target.ToArray();
                }
                Save(fileModel);
            }
            return View();
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
