using Microsoft.EntityFrameworkCore;
using SalonTime.Data;
using SalonTime.Models;

var builder = WebApplication.CreateBuilder(args);

//  services to the container.
builder.Services.AddControllersWithViews();

//  SESSIONS
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

//  SEEDING -  frisörer om databasen är tom
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    
    if (!context.Hairdressers.Any())
    {
        context.Hairdressers.Add(new Hairdresser { Name = "Anna Andersson" });
        context.Hairdressers.Add(new Hairdresser { Name = "Erik Eriksson" });
        context.Hairdressers.Add(new Hairdresser { Name = "Maria Karlsson" });
        
        await context.SaveChangesAsync();
        Console.WriteLine("Frisörer har lagts till i databasen!");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

//  DENNA FÖR SESSIONS
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();