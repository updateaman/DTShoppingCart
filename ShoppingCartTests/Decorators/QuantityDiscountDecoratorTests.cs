using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShoppingCartDAL.Models;
using ShoppingCartServices.Decorators;
using Xunit;

namespace ShoppingCartTests.Decorators
{
    public class QuantityDiscountDecoratorTests
    {
        [Fact]
        public void Calculate_returns_zero_for_emptylist()
        {
            var decorator = new QuantityDiscountDecorator(null, "", 0, 0);
            var result = decorator.Calculate(new List<ShoppingCartItem>());

            Assert.Equal(0, result);
        }

        [Theory]
        [InlineData("ItemA", 2, 1, 11)]
        [InlineData("ItemB", 7, 3, 19.98)]
        [InlineData("ItemC", 10, 5, 33.33)]
        [InlineData("ItemD", 15, 10, 0)]
        public void Calculate_returns_discount_for_correct_item(string itemName, int quantity, int discount, decimal expected)
        {
            var lstItems = new List<ShoppingCartItem>
            {
                new ShoppingCartItem{Id = 1, Product = new Product{Id = 10, Name = "ItemA", Price = 1.1M}, Quantity = 20},
                new ShoppingCartItem{Id = 2, Product = new Product{Id = 11, Name = "ItemB", Price = 2.22M}, Quantity = 21},
                new ShoppingCartItem{Id = 3, Product = new Product{Id = 12, Name = "ItemC", Price = 3.333M}, Quantity = 22}
            };

            var decorator = new QuantityDiscountDecorator(null, itemName, quantity, discount);
            var result = decorator.Calculate(lstItems);

            Assert.Equal(expected, result);
        }
    }
}
