namespace SalonTime.Models;

public class FixedAmountDiscountStrategy : IDiscountStrategy
{
    private readonly decimal _amount;  // Sparar rabattsumman från chatgpt hela koden
    
    public FixedAmountDiscountStrategy(decimal amount)
    {
        _amount = amount;
    }
    
    //  ANNORLUNDA BERÄKNING 
    public decimal ApplyDiscount(decimal originalPrice)
    {
        // Drar av fast belopp från originalpris
        // Math.Max säkerställer att priset inte blir negativt
        // Ex: 500 kr - 50 kr = 450 kr
        return Math.Max(0, originalPrice - _amount);
    }
    
    public string GetDiscountDescription()
    {
        return $"{_amount} kr rabatt";  // Ex: "50 kr rabatt"
    }
}
