using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ShoppingCartDAL.Models;
using ShoppingCartDAL.Repos;
using ShoppingCartServices.Decorators;
using ShoppingCartServices.Interfaces;
using ShoppingCartWebsite.Models;

namespace ShoppingCartServices
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepo _shoppingCartRepo;
        private readonly IDiscountFactory _discountFactory;

        public ShoppingCartService(IShoppingCartRepo shoppingCartRepo,
            IDiscountFactory discountFactory)
        {
            _shoppingCartRepo = shoppingCartRepo;
            _discountFactory = discountFactory;
        }

        public async Task<ShoppingCartItemVM> GetCartAsync()
        {
            var items = await _shoppingCartRepo.GetAllAsync();
            var discounts = _discountFactory.GetDiscounts();

            var model = new ShoppingCartItemVM
            {
                ShoppingCartItems = items,
                GrossTotal = CalculateTotalPrice(items),
                DiscountTotal = discounts.Calculate(items)
            };

            return model;
        }

        public async Task AddItemAsyc(int productId)
        {
            var item = await _shoppingCartRepo.GetByProductIdAsync(productId);
            if (item != null)
            {
                item.Quantity += 1;
                await _shoppingCartRepo.UpdateAsync(item);
            }
            else
            {
                item = new ShoppingCartItem
                {
                    ProductId = productId,
                    Quantity = 1
                };

                await _shoppingCartRepo.InsertAsync(item);
            }
        }

        public async Task UpdateItemAsync(ShoppingCartItem item)
        {
            var product = await _shoppingCartRepo.GetByProductIdAsync(item.ProductId);
            if (product == null)
            {
                throw new ArgumentException($"ProductId {item.ProductId} not found");
            }

            product.Quantity = item.Quantity;
            await _shoppingCartRepo.UpdateAsync(product);
        }

        private static decimal CalculateTotalPrice(IEnumerable<ShoppingCartItem> cartItems)
        {
            return cartItems.Sum(c => c.Quantity * c.Product.Price);
        }

    }
}
