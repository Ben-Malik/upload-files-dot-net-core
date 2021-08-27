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
    // <summary>
    // A class unit testing the FileController class.
    // </summary>
    public class FileControllerTest
    {
        [Fact]
        public void setUp()
        {
        }
        
        [Fact]
        public void testSaveFileModel_ReturnsBadRequestObjectWhenTheContentOfTheFileIsNull()
        {
            // Data statment
            var model = new FileModel { CreatedAt = DateTime.UtcNow,
                                        FileName = "Ben.txt",
                                        FileSize = 123, FileType = "txt",
                                        FileContent = null
                                      };

            var fileController = new FileController(new FileContext());

            // Call statment.
            var result = fileController.Save(model);

            // Assertion statement
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void testSaveFileModel_ReturnsFileModelWhenTheGivenFileIsValid()
        {
            // Data Satement
            byte[] fileContent = {};
            var model = new FileModel
            {
                CreatedAt = DateTime.UtcNow,
                FileName = "Ben.txt",
                FileSize = 123,
                FileType = "txt",
                FileContent = fileContent
            };

            var fileController = new FileController(new FileContext());

            // Call Statment
            var result = fileController.Save(model);

            // Assertion Statement
            Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.NotNull(result);
        }
    }
}
