using Microsoft.AspNetCore.Mvc;
using ST10256859_CLDV6211_POE.Models;

namespace ST10256859_CLDV6211_POE.Controllers
{
    public class UserController : Controller
    {
        userTBL usrTBL = new userTBL();

        [HttpPost]
        public ActionResult SignUp(userTBL Users)
        {
            var result = usrTBL.insertUser(Users);
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}