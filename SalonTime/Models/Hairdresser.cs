namespace SalonTime.Models;

public class Hairdresser
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}