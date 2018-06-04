using System;
using System.Collections.Generic;
using System.Text;
using ShoppingCartDAL.Models;

namespace ShoppingCartServices
{
    public abstract class DiscountBaseDecorator
    {
        private readonly DiscountBaseDecorator _base;
        public DiscountBaseDecorator(DiscountBaseDecorator discountBase)
        {
            _base = discountBase;
        }

        public virtual decimal Calculate(IEnumerable<ShoppingCartItem> cartItems)
        {
            return _base?.Calculate(cartItems) ?? 0;
        }
    }
}
