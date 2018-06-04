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
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _shoppingCartService.GetCartAsync();
            return View(model);
        }


        public async Task<IActionResult> Add(int productId)
        {
            await _shoppingCartService.AddItemAsyc(productId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(ShoppingCartItem item)
        {
            await _shoppingCartService.UpdateItemAsync(item);

            return RedirectToAction(nameof(Index));
        }
    }
}