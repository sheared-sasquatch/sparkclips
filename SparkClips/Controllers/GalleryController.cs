using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SparkClips.Services.Repositories;
using SparkClips.Models.HairyDatabase;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SparkClips.Controllers
{
    public class GalleryController : Controller
    {
        private IGalleryRepository _galleryRepository;

        public GalleryController(IGalleryRepository galleryRepository)
        {
            _galleryRepository = galleryRepository;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            List<GalleryEntry> galleryEntries = await _galleryRepository.GetGalleryEntries();
            // loop over each gallery entry and add any computed fields
            foreach (GalleryEntry galleryEntry in galleryEntries)
            {
                // setting the thumbnail image
                galleryEntry.Thumbnail = _galleryRepository.ComputeThumbnail(galleryEntry);
                // seting the gallery entry number of likes
                galleryEntry.Likes = await _galleryRepository.ComputeNLikes(galleryEntry);
            }
            return View(galleryEntries);
        }

        // GET: /<controller>/Detail/1
        public async Task<IActionResult> Detail(int ID)
        {
            GalleryEntry galleryEntry = await _galleryRepository.GetGalleryEntryByID(ID);
            if (galleryEntry == null)
            {
                return NotFound();
            }
            return View(galleryEntry);
        }
    }
}
