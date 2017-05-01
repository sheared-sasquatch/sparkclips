using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using SparkClips.Data;
using SparkClips.Models.HairyDatabase;
using SparkClips.Services.BlobBob;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace sparkclips.Controllers
{
    public class UploadFilesController : Controller
    {
        private IFileStorage _fileStorage;
        private SparkClipsContext _sparkClipsContext;

        public UploadFilesController(IFileStorage fileStorage, SparkClipsContext sparkClipsContext)
        {
            _fileStorage = fileStorage; // store a reference to the file storage object from ASP .NET's dependency injection container
            _sparkClipsContext = sparkClipsContext;
        }

        /// <summary>
        /// Receive one or more files from the user's POST request
        /// and upload them to blob storage
        /// </summary>
        /// <param name="files">List of FormFile objects that will
        /// get passed to the FileStorage.UploadImage method</param>
        /// <returns>A JSON object which gives some useless data about the files uploaded.
        /// This should probably become a redirect at some point.</returns>
        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            foreach (FormFile formFile in files)
            {
                if (formFile.Length > 0)
                {
                    Image image = await _fileStorage.UploadImage(ContainerName.Gallery, formFile);
                    Debug.WriteLine(image.Url);
                    Debug.WriteLine(image.Guid);
                    _sparkClipsContext.Images.Add(image);
                    _sparkClipsContext.SaveChanges();
                    Debug.WriteLine("ID: " + image.ImageID);
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size});
        }
    }
}
