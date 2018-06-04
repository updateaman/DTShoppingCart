using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingCartDAL.Models;
using ShoppingCartDAL.Repos;
using ShoppingCartServices;
using ShoppingCartWebsite.Models;

namespace ShoppingCartWebsite.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartRepo _shoppingCartRepo;
        private readonly IPriceCalculator _priceCalculator;

        public ShoppingCartController(IShoppingCartRepo shoppingCartRepo, IPriceCalculator priceCalculator)
        {
            _shoppingCartRepo = shoppingCartRepo;
            _priceCalculator = priceCalculator;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _shoppingCartRepo.GetAllAsync();
            var model = new ShoppingCartItemVM
            {
                ShoppingCartItems = items,
                Total = _priceCalculator.CalculatePrice(items)
            };
            return View(model);
        }


        public async Task<IActionResult> Add(int productId)
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

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(ShoppingCartItem item)
        {
            var product = await _shoppingCartRepo.GetByProductIdAsync(item.ProductId);
            if (product == null)
            {
                throw new ArgumentException($"ProductId {item.ProductId} not found");
            }

            product.Quantity = item.Quantity;
            await _shoppingCartRepo.UpdateAsync(product);

            return RedirectToAction(nameof(Index));
        }
    }
}