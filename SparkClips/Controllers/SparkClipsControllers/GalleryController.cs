using Microsoft.AspNetCore.Mvc;
using SparkClips.Models.HairyDatabase;
using SparkClips.Services.Repositories;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult> Index(List<int> tags)
        {
            IEnumerable<GalleryEntry> galleryEntries = await _galleryRepository.GetGalleryEntries(tags);
            galleryEntries = galleryEntries.Where(galleryEntry => 
                tags.All(tag => galleryEntry.Tags.Select(t => t.TagID).Contains(tag)));

            // loop over each gallery entry and add any computed fields
            foreach (GalleryEntry galleryEntry in galleryEntries)
            {
                // setting the thumbnail image
                galleryEntry.Thumbnail = _galleryRepository.ComputeThumbnail(galleryEntry);
                // seting the gallery entry number of likes
                galleryEntry.Likes = await _galleryRepository.ComputeNLikes(galleryEntry);
                // Boolean has the picture been favorited by this user already
                //galleryEntry.Faved = _galleryRepository.isFavorited(galleryEntry);
            }
            galleryEntries = galleryEntries.OrderByDescending(galleryEntry => galleryEntry.Likes);
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
