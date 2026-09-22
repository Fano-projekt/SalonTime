using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalonTime.Data;
using SalonTime.Models;
using SalonTime.Services;

namespace SalonTime.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            // DI hanterar DbContext livscykel - ingen manuell disposal needed

            _context = context;
        }

        // Hjälpmetod för att kolla inloggning
        private bool IsAdminLoggedIn()
        {
            return HttpContext.Session.GetString("IsAdmin") == "true";
        }

        // GET: /Booking/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Services = await _context.Services
                .Select(s => new SelectListItem 
                { 
                    Value = s.Id.ToString(), 
                    Text = $"{s.Name} - {s.Price} kr" 
                })
                .ToListAsync();

            ViewBag.Hairdressers = await _context.Hairdressers
                .Select(h => new SelectListItem 
                { 
                    Value = h.Id.ToString(), 
                    Text = h.Name 
                })
                .ToListAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking booking, string discountType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Async/await frigör trådar under databasanrop
                    var service = await _context.Services.FindAsync(booking.ServiceId);
                    booking.OriginalPrice = service.Price;
                    
                    // Strategy Pattern - ORIGINAL version
                    // DiscountService är ett enkelt objekt - GC hanterar automatiskt
                    var discountService = new DiscountService();
                    
                    switch(discountType)
                    {
                        case "Percentage10":
                            discountService.SetDiscountStrategy(new PercentageDiscountStrategy(10));
                            booking.DiscountType = "Percentage10";
                            break;
                        case "Fixed50":
                            discountService.SetDiscountStrategy(new FixedAmountDiscountStrategy(50));
                            booking.DiscountType = "Fixed50";
                            break;
                        default:
                            discountService.SetDiscountStrategy(new NoDiscountStrategy());
                            booking.DiscountType = "None";
                            break;
                    }
                    
                    booking.FinalPrice = discountService.CalculateFinalPrice(service.Price);
                    booking.DiscountDescription = discountService.GetDiscountDescription();
                     //Fick rättning av deepssek för denna funktinen
                    _context.Bookings.Add(booking);
                    await _context.SaveChangesAsync();
            
                    Console.WriteLine($"✅ BOKNING SPARAD: #{booking.Id} för {booking.CustomerName}");
                    Console.WriteLine($"   Pris: {booking.OriginalPrice} kr → {booking.FinalPrice} kr ({booking.DiscountDescription})");
            
                    TempData["SuccessMessage"] = "Bokningen är genomförd!";
                    return RedirectToAction("Confirmation", new { id = booking.Id });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ FEL VID SPARANDE: {ex.Message}");
                    ModelState.AddModelError("", "Kunde inte spara bokningen. Försök igen.");
                }
            }

            // Reload dropdown data...
            ViewBag.Services = await _context.Services
                .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = $"{s.Name} - {s.Price} kr" })
                .ToListAsync();

            ViewBag.Hairdressers = await _context.Hairdressers
                .Select(h => new SelectListItem { Value = h.Id.ToString(), Text = h.Name })
                .ToListAsync();

            return View(booking);
        }

        // GET: /Booking/Confirmation/5
        public async Task<IActionResult> Confirmation(int id)
        {
           
            // Smart minneshantering - capture enkelt ID istället för komplext closure
            var bookingId = id;
    
            var booking = await _context.Bookings
                .Include(b => b.Service)
                .Include(b => b.Hairdresser)
                .FirstOrDefaultAsync(b => b.Id == bookingId);
            
            if (booking == null)
            {
                return NotFound();
            }

            Console.WriteLine($" Bokning #{booking.Id} visad för {booking.CustomerName}");
            Console.WriteLine($"   Pris: {booking.OriginalPrice} kr → {booking.FinalPrice} kr ({booking.DiscountDescription})");

            return View(booking);
        }

        // GET: /Booking/AllBookings - För admin
        public async Task<IActionResult> AllBookings()
        {
            if (!IsAdminLoggedIn())
            {
                TempData["ErrorMessage"] = "Du måste logga in som admin!";
                return RedirectToAction("Index", "Login");
            }

            var bookings = await _context.Bookings
                .Include(b => b.Service)
                .Include(b => b.Hairdresser)
                .OrderByDescending(b => b.BookingDate)
                .ThenBy(b => b.BookingTime)
                .ToListAsync();

            return View(bookings);
        }
    }
}