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
            return RedirectToAction("MyWork", "Home");
        }

        [HttpGet]
        public ActionResult MyWork()
        {
            return View(prodTBL);
        }

        public ActionResult ProductAdd()
        {
            return View();
        }
    }
}