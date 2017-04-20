using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace sparkclips.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Knowledge() {
            return View();
        }

        public IActionResult Gallery()
        {
            return View();
        }

        public IActionResult Log()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
