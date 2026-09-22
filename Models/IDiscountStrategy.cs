namespace SalonTime.Models;

public interface IDiscountStrategy 
{
    // Denna metod MÅSTE finnas i alla rabattklasser
    // Den tar originalpris och returnerar rabatterat pris (Deepseek)
    decimal ApplyDiscount(decimal originalPrice);
    
    // Beskriver rabatten för användaren
    string GetDiscountDescription();
}


