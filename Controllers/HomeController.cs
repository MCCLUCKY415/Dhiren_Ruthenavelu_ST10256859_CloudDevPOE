using Microsoft.AspNetCore.Mvc;
using ST10256859_CLDV6211_POE.Models;
using ST10256859_CLDV6211_POE_Part1.Models;
using System.Diagnostics;

namespace ST10256859_CLDV6211_POE_Part1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
            return View();
        }

        public IActionResult AboutUs()
        {
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
            return View();
        }

        public IActionResult ContactUs()
        {
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}