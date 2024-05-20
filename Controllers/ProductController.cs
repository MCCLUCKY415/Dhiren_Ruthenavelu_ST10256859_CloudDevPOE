using Microsoft.AspNetCore.Mvc;
using ST10256859_CLDV6211_POE.Models;

namespace ST10256859_CLDV6211_POE.Controllers
{
    public class ProductController : Controller
    {
        public productTBL prodTBL = new productTBL();

        [HttpPost]
        public ActionResult ProductAdd(productTBL products)
        {
            var result2 = prodTBL.InsertProduct(products);
            return RedirectToAction("MyWork", "Product");
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
            return View(products);
        }

        public ActionResult ProductAdd()
        {
            return View();
        }
    }
}