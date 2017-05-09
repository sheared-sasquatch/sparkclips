using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SparkClips.Controllers
{
    public class KnowledgeController : Controller
    {
        // GET: /Knowledge/
        public IActionResult Index()
        {
            return View();
        }
    }
}
