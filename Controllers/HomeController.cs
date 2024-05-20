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
            productTBL product = new productTBL();

            int? UserID = HttpContext.Session.GetInt32("UserID");
            if (!UserID.HasValue)
            {
                return View();
            }
            userTBL user = new userTBL();
            string FullName = user.GetFullName((int)UserID);

            if (!string.IsNullOrEmpty(FullName))
            {
                ViewData["WelcomeMessage"] = $"Welcome, {FullName}!";
            }

            List<productTBL> products = product.GetAllProducts();

            ViewData["UserID"] = UserID.Value;

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}