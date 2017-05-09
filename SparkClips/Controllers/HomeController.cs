using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SparkClips.Data;
using SparkClips.Models.HairyDatabase;
using Microsoft.EntityFrameworkCore;
using SparkClips.Services.Repositories;

namespace SparkClips.Controllers
{
    public class HomeController : Controller
    {
        private IGalleryRepository _galleryRepository;

        public HomeController(IGalleryRepository galleryRepository)
        {
            _galleryRepository = galleryRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Knowledge()
        {
            return View();
        }

        public async Task<IActionResult> Gallery()
        {
            List<GalleryEntry> galleryEntries = await _galleryRepository.GetGalleryEntries();

            // loop over each gallery entry and add any computed fields
            foreach(GalleryEntry galleryEntry in galleryEntries)
            {
                // setting the thumbnail image
                galleryEntry.Thumbnail = _galleryRepository.ComputeThumbnail(galleryEntry);

                // seting the gallery entry number of likes
                galleryEntry.Likes = await _galleryRepository.ComputeNLikes(galleryEntry);
            }

            return View(galleryEntries);
        }

        public async Task<IActionResult> GalleryDetail(int ID)
        {
            GalleryEntry galleryEntry = await _galleryRepository.GetGalleryEntryByID(ID);

            if (galleryEntry == null)
            {
                return NotFound();
            }

            return View(galleryEntry);
        }

        public IActionResult Log()
        {
            return View();
        }

        public IActionResult Admin()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
