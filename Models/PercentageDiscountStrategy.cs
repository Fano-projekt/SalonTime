namespace SalonTime.Models
{
    public class PercentageDiscountStrategy : IDiscountStrategy
    {
        private readonly decimal _percentage;
        
        // Konstruktor - tar emot hur många procent rabatt (Deepseek)
        public PercentageDiscountStrategy(decimal percentage)
        {
            _percentage = percentage;
        }
        
        // Implementerar interface-metoden
        public decimal ApplyDiscount(decimal originalPrice)
        {
            // Beräknar: originalpris * (1 - rabattprocent/100)
            // Ex: 500 kr * (1 - 10/100) = 500 * 0.9 = 450 kr
            return originalPrice * (1 - _percentage / 100);
        }
        
        public string GetDiscountDescription()
        {
            return $"{_percentage}% rabatt";
        }
    }
}