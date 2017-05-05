using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Moq;
using sparkclips.Controllers;
using SparkClips.Data;
using SparkClips.Models;
using SparkClips.Services.BlobBob;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SparkClips.Tests
{
    public class UploadControllerTests
    {

        public async Task Index()
        {
            // arrange
            var mockFileStorage = new Mock<IFileStorage>();
            var applicationDbContext = new Mock<ApplicationDbContext>();

            var controller = new UploadFilesController(mockFileStorage.Object, applicationDbContext.Object);

            // act
            var result = controller.Post();

            // assert
        }

    }
}
