using Microsoft.AspNetCore.Mvc;

namespace SparkClips.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("/gallery");
        }

        public IActionResult Knowledge()
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
