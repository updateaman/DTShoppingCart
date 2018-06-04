using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShoppingCartDAL.Models;
using ShoppingCartDAL.Repos;
using ShoppingCartWebsite.Controllers;
using Xunit;

namespace ShoppingCartTests.ControllerTests
{
    //Just creating one controller test for demo
    public class HomeControllerTests
    {
        [Fact]
        public async Task Index_returns_view()
        {
            var repoMock = new Mock<IProductRepository>();
            var controller = new HomeController(repoMock.Object);

            var result = await controller.Index();
            var actualView = result as ViewResult;

            Assert.NotNull(actualView);
            Assert.IsAssignableFrom<IEnumerable<Product>>(actualView.Model);
        }

        [Fact]
        public async Task Index_returns_list_of_properties()
        {
            var lstProduct = new List<Product>
            {
                new Product{ Id=1, Name = "ItemA", Price = 1.1M},
                new Product{ Id=2, Name = "ItemB", Price = 2.22M},
                new Product{ Id=2, Name = "ItemC", Price = 3.333M},
            };

            var repoMock = new Mock<IProductRepository>();
            repoMock.Setup(r => r.GetAll()).ReturnsAsync(lstProduct);

            var controller = new HomeController(repoMock.Object);

            var index = await controller.Index();
            var actualView = index as ViewResult;

            var result = actualView?.Model as IEnumerable<Product>;
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());

        }
    }
}

