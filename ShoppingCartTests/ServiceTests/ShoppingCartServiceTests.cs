using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using ShoppingCartDAL.Models;
using ShoppingCartDAL.Repos;
using ShoppingCartServices;
using ShoppingCartServices.Interfaces;
using Xunit;

namespace ShoppingCartTests.ServiceTests
{
    public class ShoppingCartServiceTests
    {
        [Fact]
        public async Task GetCartAsync_returns_cart_items()
        {
            var lstItems = new List<ShoppingCartItem>
            {
                new ShoppingCartItem{Id = 1, Product = new Product{Id = 10, Name = "ItemA", Price = 1.1M}, Quantity = 20},
                new ShoppingCartItem{Id = 2, Product = new Product{Id = 11, Name = "ItemB", Price = 2.22M}, Quantity = 21},
                new ShoppingCartItem{Id = 3, Product = new Product{Id = 12, Name = "ItemC", Price = 3.333M}, Quantity = 22}
            };
            var repoMock = new Mock<IShoppingCartRepo>();
            repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(lstItems);

            var discounts = new Mock<DiscountBaseDecorator>(null);
            var factoryMock = new Mock<IDiscountFactory>();
            factoryMock.Setup(f => f.GetDiscounts()).Returns(discounts.Object);
            var cartService = new ShoppingCartService(repoMock.Object, factoryMock.Object);

            var result = await cartService.GetCartAsync();

            Assert.NotNull(result);
            Assert.Equal(lstItems.Count, result.ShoppingCartItems.Count());
            Assert.Equal(lstItems.Sum(l => l.Product.Price), result.GrossTotal);

        }
    }
}
