using Microsoft.AspNetCore.Mvc;
using SalonTime.Data;
using SalonTime.Models;
using Microsoft.EntityFrameworkCore;

namespace SalonTime.Controllers;

public class ServiceController : Controller
{
    private readonly ApplicationDbContext _context;

    public ServiceController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: /Service/
    public async Task<IActionResult> Index()
    {
        var services = await _context.Services.ToListAsync();
        return View(services);
    }
}