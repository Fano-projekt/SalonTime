using Microsoft.AspNetCore.Mvc;
using SalonTime.Data;
using SalonTime.Models;

namespace SalonTime.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hjälpmetod för att kolla inloggning
        private bool IsAdminLoggedIn()
        {
            return HttpContext.Session.GetString("IsAdmin") == "true";
        }

        // GET: Admin/CreateService
        public IActionResult CreateService()
        {
            if (!IsAdminLoggedIn())
            {
                TempData["ErrorMessage"] = "Du måste logga in som admin!";
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        // POST: Admin/CreateService
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateService(Service service)
        {
            if (!IsAdminLoggedIn())
            {
                TempData["ErrorMessage"] = "Du måste logga in som admin!";
                return RedirectToAction("Index", "Login");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Services.Add(service);
                    await _context.SaveChangesAsync();
                    
                    Console.WriteLine($"Tjänsten '{service.Name}' sparades i databasen!");
                    TempData["SuccessMessage"] = $"Tjänsten '{service.Name}' har lagts till!";
                    return RedirectToAction("Index", "Service");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fel vid sparande: {ex.Message}");
                    ModelState.AddModelError("", "Kunde inte spara tjänsten. Försök igen.");
                }
            }
            else
            {
                Console.WriteLine("ModelState är ogiltig!");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Valideringsfel: {error.ErrorMessage}");
                }
            }

            return View(service);
        }
    }
}