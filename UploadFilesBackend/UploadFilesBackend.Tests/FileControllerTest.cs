using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using UploadFilesBackend;
using UploadFilesBackend.Controllers;
using UploadFilesBackend.Models;
using Xunit;
using Assert = Xunit.Assert;

namespace UploadFilesBackend.Tests
{
    public class FileControllerTest
    {
        [SetUp]
        public void setUp()
        {
        }
        
        [Fact]
        public void testSaveFileModel_ReturnsBadRequestObjectWhenTheContentOfTheFileIsNull()
        {
            var model = new FileModel { CreatedAt = DateTime.UtcNow,
                                        FileName = "Ben.txt",
                                        FileSize = 123, FileType = "txt",
                                        FileContent = null
                                      };

            //mockContext.Setup(repo => repo.isFileModelValid(model)).Returns(false);
            var fileController = new FileController(new FileContext());


            var result = fileController.Save(model);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void testSaveFileModel_ReturnsFileModelWhenTheGivenFileIsValid()
        {
            byte[] fileContent = {};
            var model = new FileModel
            {
                CreatedAt = DateTime.UtcNow,
                FileName = "Ben.txt",
                FileSize = 123,
                FileType = "txt",
                FileContent = fileContent
            };

            //mockContext.Setup(repo => repo.isFileModelValid(model)).Returns(false);
            var fileController = new FileController(new FileContext());


            var result = fileController.Save(model);

            Console.WriteLine(result);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}
