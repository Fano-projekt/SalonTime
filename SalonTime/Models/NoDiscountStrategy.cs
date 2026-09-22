namespace SalonTime.Models;

public class NoDiscountStrategy : IDiscountStrategy
{
        public decimal ApplyDiscount(decimal originalPrice)
        {
            // Returnerar originalpris oförändrat
            return originalPrice;
        }
        
        public string GetDiscountDescription()
        {
            return "Ingen rabatt";
    }
}

