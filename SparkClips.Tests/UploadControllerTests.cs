using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Moq;
using sparkclips.Controllers;
using SparkClips.Data;
using SparkClips.Models;
using SparkClips.Models.HairyDatabase;
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
        [Fact]
        public async Task Index()
        {
            // arrange
            var mockFileStorage = new Mock<IFileStorage>();
            mockFileStorage
                .Setup(x => x.UploadImage(It.IsAny<ContainerName>(), It.IsAny<FormFile>()))
                .ReturnsAsync(new Image {
                    Url = "link.com",
                    Guid = Guid.NewGuid(),
                    ContainerName = 0,
                });

            //var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //optionsBuilder.Use
            //var _dbContext = new ApplicationDbContext(optionsBuilder.Options);

            var mockApplicationDbContext = new Mock<ApplicationDbContext>();

            var controller = new UploadFilesController(mockFileStorage.Object, mockApplicationDbContext.Object);

            var formFiles = new List<IFormFile>();

            // act
            var result = await controller.Post(formFiles);

            // assert
            mockApplicationDbContext.Verify(
                foo => foo.Images.Add(It.IsAny<Image>()), Times.AtLeastOnce()
                );
        }

    }
}
