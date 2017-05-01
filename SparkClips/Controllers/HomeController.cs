using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SparkClips.Data;
using SparkClips.Models.HairyDatabase;

namespace SparkClips.Controllers
{
    public class HomeController : Controller
    {
        private SparkClipsContext _sparkClipsContext;

        public HomeController(SparkClipsContext sparkClipsContext)
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

        public IActionResult Gallery()
        {
            List<GalleryEntry> galleryEntries = _sparkClipsContext.GalleryEntries.ToList();
            List<Image> images = new List<Image>();
            foreach(GalleryEntry galleryEntry in galleryEntries)
            {
                IEnumerable<int> image_ids = _sparkClipsContext.GalleryEntry_Image
                    .Where(x => x.GalleryEntryID == galleryEntry.GalleryEntryID)
                    .ToList()
                    .Select(x => x.ImageID);

                foreach(int pk in image_ids)
                {
                    Image image = _sparkClipsContext.Images.Single(i => i.ImageID == pk);
                    images.Add(image);
                }
            }
            return View();
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
