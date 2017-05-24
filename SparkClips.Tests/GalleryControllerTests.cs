using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using SparkClips.Controllers;
using SparkClips.Data;
using SparkClips.Models;
using SparkClips.Models.HairyDatabase;
using SparkClips.Services.BlobBob;
using SparkClips.Services.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SparkClips.Tests
{
    public class GalleryControllerTests
    {
        [Fact]
        public async Task Index()
        {
            // arrange
            var mockGalleryRepository = new Mock<IGalleryRepository>();
            mockGalleryRepository
                .Setup(x => x.GetGalleryEntries())
                .Returns(Task.FromResult(GetTestSessions()));

            var mockTagRepository = new Mock<ITagRepository>();
            mockTagRepository
                .Setup(x => x.GetTags())
                .Returns(Task.FromResult(GetTestTags()));


            var controller = new GalleryController(mockGalleryRepository.Object, mockTagRepository.Object);

            // act
            var result = await controller.Index(null);

            // assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<GalleryEntry>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Detail()
        {
            // arrange
            var test_pk = 2;

            var mockGalleryRepository = new Mock<IGalleryRepository>();
            mockGalleryRepository
                .Setup(x => x.GetGalleryEntries())
                .Returns(Task.FromResult(GetTestSessions()));

            var mockTagRepository = new Mock<ITagRepository>();
            mockTagRepository
                .Setup(x => x.GetTags())
                .Returns(Task.FromResult(GetTestTags()));


            var controller = new GalleryController(mockGalleryRepository.Object, mockTagRepository.Object);


            // act
            var result = await controller.Detail(test_pk);

            // assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<GalleryEntry>(
                viewResult.ViewData.Model);
            Assert.Equal("The Combover", model.Title);
            Assert.Equal(test_pk, model.GalleryEntryID);
        }


        private IEnumerable<GalleryEntry> GetTestSessions()
        {
            var sessions = new List<GalleryEntry>();
            sessions.Add(new GalleryEntry
            {
                GalleryEntryID = 1,
                Title = "The Pompadour",
                Description = "You cut it well",
                Instructions = "You use a 6",
            });

            sessions.Add(new GalleryEntry
            {
                GalleryEntryID = 2,
                Title = "The Combover",
                Description = "You cut it well",
                Instructions = "You use a 6",
            });

            return sessions;
        }

        private IEnumerable<Tag> GetTestTags()
        {
            var tags = new List<Tag>();
            tags.Add(new Tag
            {
                TagID = 1,
                Name = "short hair"
            });
            return tags;
        }


    }
}
