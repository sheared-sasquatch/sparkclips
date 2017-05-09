using Microsoft.AspNetCore.Mvc;
using SparkClips.Models.HairyDatabase;
using SparkClips.Services.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SparkClips.Controllers
{
    public class GalleryController : Controller
    {
        private IGalleryRepository _galleryRepository;

        public GalleryController(IGalleryRepository galleryRepository)
        {
            _galleryRepository = galleryRepository;
        }

        // GET: /Gallery/
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

        // GET: /Gallery/Detail/1
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
