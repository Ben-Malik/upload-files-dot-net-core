using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using UploadFilesBackend.Models;  
namespace UploadFilesBackend.Services
{
    // <summary>
    //  A manager class to handle the role of bridge between the db and the controller.
    // </summary>
    //
    // TODO Should be made as an interface and follow repository pattern.
    //
    public class FileManager
    {
        private readonly FileContext _fileContext;

        public FileManager(FileContext fileContext)
        {
            _fileContext = fileContext;
        }

        // <summary>
        // Grabs all Uploaded file models
        // </summary>
        //
        //<return>
        // a list of @FileModels.
        //</return>
        public List<FileModel> getFiles()
        {
            return _fileContext.Files.ToList();
        }

        //<summary>
        // Given a file, extracts needed information, creates a FileModel object
        // and saves it to the database.
        //</summary>
        public async void UploadFileToDB(IFormFile file)
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
                _fileContext.Files.Add(fileModel);
                _fileContext.SaveChanges();
            }
        }

    }
}
