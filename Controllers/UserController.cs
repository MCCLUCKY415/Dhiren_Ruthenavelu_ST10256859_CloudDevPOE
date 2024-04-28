using Microsoft.AspNetCore.Mvc;
using ST10256859_CLDV6211_POE.Models;

namespace ST10256859_CLDV6211_POE.Controllers
{
    public class UserController : Controller
    {
        private userTBL usrTBL = new userTBL();

        [HttpPost]
        public ActionResult SignUp(userTBL Users)
        {
            var result = usrTBL.InsertUser(Users);
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}