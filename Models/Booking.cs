using System.ComponentModel.DataAnnotations;

namespace SalonTime.Models;

public class Booking
{
    public int Id { get; set; }

    // Kundinformation
    [Required(ErrorMessage = "Namn är obligatoriskt")]
    public string CustomerName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "E-post är obligatoriskt")]
    [EmailAddress(ErrorMessage = "Ogiltig e-postadress")]
    public string CustomerEmail { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Telefon är obligatoriskt")]
    public string CustomerPhone { get; set; } = string.Empty;

    // Bokningsinformation - BARA ID:n är required, inte hela objekten
    [Required(ErrorMessage = "Välj en tjänst")]
    public int ServiceId { get; set; }
    public Service? Service { get; set; }  

    [Required(ErrorMessage = "Välj en frisör")]
    public int HairdresserId { get; set; }
    public Hairdresser? Hairdresser { get; set; }  

    [Required(ErrorMessage = "Välj ett datum")]
    public DateTime BookingDate { get; set; }

    [Required(ErrorMessage = "Välj en tid")]
    public string BookingTime { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string Status { get; set; } = "Bokad";
    
    public decimal OriginalPrice { get; set; }
    
    public decimal FinalPrice { get; set; }
    
    public string DiscountType { get; set; } = "Ingen";
    
    public string DiscountDescription { get; set; } = "Ingen rabatt";
}