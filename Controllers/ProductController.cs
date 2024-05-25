using Microsoft.AspNetCore.Mvc;
using ST10256859_CLDV6211_POE.Models;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ST10256859_CLDV6211_POE.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public productTBL prodTBL = new productTBL();

        public ProductController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public ActionResult ProductAdd(productTBL product)
        {
            var result2 = prodTBL.InsertProduct(product, _webHostEnvironment);
            return RedirectToAction("MyWork");
        }

        [HttpGet]
        public ActionResult ProductAdd()
        {
            return View();
        }

        public ActionResult ProductGet(productTBL products)
        {
            var result = prodTBL.GetAllProducts();
            return View("MyWork", result);
        }

        [HttpGet]
        public ActionResult MyWork()
        {
            var products = prodTBL.GetAllProducts();
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");

            int? UserID = HttpContext.Session.GetInt32("UserID");
            if (!UserID.HasValue)
            {
                return View(products);
            }
            userTBL user = new userTBL();
            string FullName = user.GetFullName((int)UserID);

            if (!string.IsNullOrEmpty(FullName))
            {
                ViewData["WelcomeMessage"] = $"~ Welcome, {FullName} ~";
            }

            ViewData["UserID"] = UserID.Value;

            return View(products);
        }

        public FileContentResult GetProductImage(int productId)
        {
            var product = prodTBL.GetProductById(productId);
            if (product != null && !string.IsNullOrEmpty(product.ImageUrl))
            {
                var path = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl);
                byte[] imageData = System.IO.File.ReadAllBytes(path);
                return File(imageData, "image/jpeg");
            }
            else
            {
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "images/default.jpg");
                byte[] imageData = System.IO.File.ReadAllBytes(path);
                return File(imageData, "image/jpeg");
            }
        }
    }
}