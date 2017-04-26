using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sparkclips.Models;
using sparkclips.Blob;

namespace sparkclips.Controllers
{
    public class HomeController : Controller
    {
        private IBlobBob _blobBob;

        public HomeController(IBlobBob blobBob)
        {
            _blobBob = blobBob;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Knowledge() {
            return View();
        }

        public async Task<IActionResult> Gallery()
        {
            List<Image> images = await _blobBob.FetchGalleryImages();
            return View(images);
        }

        public IActionResult Log()
        {
            return View(LogEntry.GetFakeData());
        }

        public IActionResult LogDetail(int ID) {
            LogEntry logEntry = LogEntry.GetFakeData().First(item => item.ID == ID);
            return View(logEntry);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
