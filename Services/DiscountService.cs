using SalonTime.Models;

namespace SalonTime.Services
{
    public class DiscountService
    {
        private IDiscountStrategy _discountStrategy;
        
        public void SetDiscountStrategy(IDiscountStrategy discountStrategy)
        {
            _discountStrategy = discountStrategy;
        }
        
        public decimal CalculateFinalPrice(decimal originalPrice)
        {
            return _discountStrategy?.ApplyDiscount(originalPrice) ?? originalPrice;
        }
        
        public string GetDiscountDescription()
        {
            return _discountStrategy?.GetDiscountDescription() ?? "Ingen rabatt";
        }
    }
}