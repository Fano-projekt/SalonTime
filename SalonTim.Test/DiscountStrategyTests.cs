using SalonTime.Models;
using Xunit;

namespace SalonTim.Test
{
    public class DiscountStrategyTests
    {
        [Fact]
        public void PercentageDiscount_10Percent_ShouldCalculateCorrectly()
        {
            // Arrange - Förbered testet
            var strategy = new PercentageDiscountStrategy(10);
            decimal originalPrice = 500m;
            
            // Act - Utför operationen vi testar
            decimal result = strategy.ApplyDiscount(originalPrice);
            
            // Assert - Kontrollera att resultatet är korrekt
            Assert.Equal(450m, result); // 500 * 0.9 = 450
        }

        [Fact] 
        public void FixedAmountDiscount_50Kr_ShouldCalculateCorrectly()
        {
            // Arrange
            var strategy = new FixedAmountDiscountStrategy(50);
            decimal originalPrice = 500m;
            
            // Act  
            decimal result = strategy.ApplyDiscount(originalPrice);
            
            // Assert
            Assert.Equal(450m, result); // 500 - 50 = 450
        }

        [Fact]
        public void NoDiscount_ShouldReturnOriginalPrice()
        {
            // Arrange
            var strategy = new NoDiscountStrategy();
            decimal originalPrice = 500m;
            
            // Act
            decimal result = strategy.ApplyDiscount(originalPrice);
            
            // Assert
            Assert.Equal(500m, result);
        }
    }
}