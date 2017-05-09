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
