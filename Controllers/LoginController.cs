using Microsoft.AspNetCore.Mvc;
using ST10256859_CLDV6211_POE.Models;

namespace ST10256859_CLDV6211_POE.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginModel login;

        public LoginController()
        {
            login = new LoginModel();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var loginModel = new LoginModel();
            int UserID = loginModel.SelectUser(email, password);
            if (UserID != -1)
            {
                return RedirectToAction("Index", "Home", new { UserID = UserID });
            }
            else
            {
                return View("LoginFailed");
            }
        }
    }
}