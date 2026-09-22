using Microsoft.AspNetCore.Mvc;

namespace SalonTime.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            // Om redan inloggad, redirecta till admin
            if (IsAdminLoggedIn())
                return RedirectToAction("CreateService", "Admin");
                
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            // Hårdkodad admin 
            if (username == "admin" && password == "admin123")
            {
                HttpContext.Session.SetString("IsAdmin", "true");
                TempData["SuccessMessage"] = "Välkommen admin!";
                return RedirectToAction("CreateService", "Admin");
            }
            
            ModelState.AddModelError("", "Ogiltigt användarnamn eller lösenord");
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("IsAdmin");
            TempData["SuccessMessage"] = "Du är utloggad!";
            return RedirectToAction("Index", "Home");
        }

        private bool IsAdminLoggedIn()
        {
            return HttpContext.Session.GetString("IsAdmin") == "true";
        }
    }
}