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

        public IActionResult Index(int UserID)
        {
            List<productTBL> products = productTBL.GetAllProducts();

            ViewData["UserID"] = UserID;

            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult MyWork()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}