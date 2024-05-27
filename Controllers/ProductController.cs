using Microsoft.AspNetCore.Mvc;
using ST10256859_CLDV6211_POE.Models;

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
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");

            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                return View();
            }
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

        [HttpPost]
        public async Task<ActionResult> PlaceOrder(int ProductId)
        {
            int? UserID = HttpContext.Session.GetInt32("UserID");

            if (!UserID.HasValue)
            {
                return RedirectToAction("Login", "User");
            }

            transactionTBL transaction = new transactionTBL();

            var product = transaction.InsertOrder((int)UserID, ProductId);

            var orderedProduct = prodTBL.GetProductById(ProductId);
            if (orderedProduct != null)
            {
                orderedProduct.Quantity -= 1;
                prodTBL.UpdateProduct(orderedProduct);
                if (orderedProduct.Quantity == 0)
                {
                    orderedProduct.Availability = false;
                    prodTBL.UpdateProduct(orderedProduct);
                }
                else
                {
                    orderedProduct.Availability = true;
                    prodTBL.UpdateProduct(orderedProduct);
                }
            }
            await Task.Delay(2000);

            return RedirectToAction("MyWork");
        }
    }
}