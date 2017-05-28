using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SparkClips.Services.BlobBob;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using SparkClips.Models.HairyDatabase;
using SparkClips.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SparkClips.Controllers.SparkClipsControllers
{
    public class GalleryAdminController : Controller
    {
        private IFileStorage _fileStorage;
        private ApplicationDbContext _sparkClipsContext;

        public GalleryAdminController(IFileStorage fileStorage, ApplicationDbContext sparkClipsContext)
        {
            _fileStorage = fileStorage;
            _sparkClipsContext = sparkClipsContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Receive one or more files from the user's POST request
        /// and upload them to blob storage
        /// </summary>
        /// <param name="files">List of FormFile objects that will
        /// get passed to the FileStorage.UploadImage method</param>
        /// <returns>A JSON object which gives some useless data about the files uploaded.
        /// This should probably become a redirect at some point.</returns>
        [HttpPost]
        public async Task<IActionResult> ProcessEntry(List<IFormFile> files)
        {


            foreach (FormFile formFile in files)
            {
                if (formFile.Length > 0)
                {
                    Image image = await _fileStorage.UploadImage(ContainerName.Gallery, formFile);
                    _sparkClipsContext.Images.Add(image);
                    _sparkClipsContext.SaveChanges();

  
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return RedirectToAction("Index");
        }
    }
}
