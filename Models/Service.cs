using System.ComponentModel.DataAnnotations;

namespace SalonTime.Models;

public class Service
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Namn är obligatoriskt")]
    public string Name { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Pris är obligatoriskt")]
    [Range(0.01, 10000, ErrorMessage = "Pris måste vara större än 0")]
    public decimal Price { get; set; }
    
    public string Duration { get; set; } = string.Empty;

    // Initiera med en tom lista istället
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}