using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SparkClips.Data;
using SparkClips.Models.HairyDatabase;
using Microsoft.EntityFrameworkCore;

namespace SparkClips.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _sparkClipsContext;

        public HomeController(ApplicationDbContext sparkClipsContext)
        {
            _sparkClipsContext = sparkClipsContext;
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
            List<GalleryEntry> galleryEntries = await _sparkClipsContext.GalleryEntries
                .Include(galleryEntry => galleryEntry.Images) // https://docs.microsoft.com/en-us/ef/core/querying/related-data#eager-loading
                    .ThenInclude(image => image.Image)
                .ToListAsync();

            // loop over each gallery entry and add any computed fields
            foreach(GalleryEntry galleryEntry in galleryEntries)
            {
                // setting the thumbnail image
                if (galleryEntry.Images.Count() == 0)
                {
                    // if this gallery entry has no images defined, return a random stock photo
                    galleryEntry.Thumbnail = "https://unsplash.it/g/200/300/?random";
                }
                else
                {
                    // get first related entry in the associative many2many table
                    GalleryEntry_Image firstImage = galleryEntry.Images.First();
                    galleryEntry.Thumbnail = firstImage.Image.Url;
                }

                var likes = await _sparkClipsContext.GalleryEntry_ApplicationUser
                    .Include(ge_au => ge_au.GalleryEntry)
                    .Include(ge_au => ge_au.ApplicationUser)
                    .Where(ge_au => ge_au.GalleryEntry == galleryEntry)
                    .ToListAsync();

                galleryEntry.Likes = likes.Count();
            }

            return View(galleryEntries);
        }

        public async Task<IActionResult> GalleryDetail(int ID)
        {
            GalleryEntry galleryEntry = await _sparkClipsContext.GalleryEntries
                .Include(ge => ge.Images)
                    .ThenInclude(image => image.Image)
                .Include(ge => ge.Tags)
                    .ThenInclude(tag => tag.Tag)
                .SingleOrDefaultAsync(g => g.GalleryEntryID == ID);

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
