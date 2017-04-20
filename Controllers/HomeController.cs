using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sparkclips.Models;

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
