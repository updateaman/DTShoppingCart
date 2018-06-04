using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ShoppingCartDAL.Models;

namespace ShoppingCartServices
{
    public class PriceCalculator : IPriceCalculator
    {
        public decimal CalculatePrice(IEnumerable<ShoppingCartItem> cartItems)
        {
            return cartItems.Sum(c => c.Quantity * c.Product.Price);
        }
    }

    public interface IPriceCalculator
    {
        decimal CalculatePrice(IEnumerable<ShoppingCartItem> cartItems);
    }
}
