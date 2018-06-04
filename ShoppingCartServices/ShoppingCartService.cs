using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ShoppingCartDAL.Models;
using ShoppingCartServices.Decorators;

namespace ShoppingCartServices
{
    public class ShoppingCartService : IShoppingCartService
    {

        public decimal CalculatePrice(IEnumerable<ShoppingCartItem> cartItems)
        {
            return cartItems.Sum(c => c.Quantity * c.Product.Price);
        }

        public DiscountBaseDecorator GetDiscountDecorators(IEnumerable<ShoppingCartItem> cartItems)
        {
            DiscountBaseDecorator baseDiscount = new QuantityDiscountDecorator(null, "Milk", 4, 1, cartItems);
            baseDiscount = new QuantityADiscountBDecorator(baseDiscount, "Butter", "Bread", 2, 0.50M, cartItems);
            return baseDiscount;
        }
    }

    public interface IShoppingCartService
    {
        decimal CalculatePrice(IEnumerable<ShoppingCartItem> cartItems);
        DiscountBaseDecorator GetDiscountDecorators(IEnumerable<ShoppingCartItem> cartItems);
    }
}
