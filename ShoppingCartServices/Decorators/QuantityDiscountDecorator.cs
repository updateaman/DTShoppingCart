using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;
using ShoppingCartDAL.Models;

namespace ShoppingCartServices.Decorators
{
    public class QuantityDiscountDecorator : DiscountBaseDecorator
    {
        private readonly string _qualifyingProductName;
        private readonly int _qualifyingQuantity;
        private readonly int _discountQuantity;
        private readonly IEnumerable<ShoppingCartItem> _cartItems;

        public QuantityDiscountDecorator(DiscountBaseDecorator discountBase,
            string productName,
            int quantity,
            int discount,
            IEnumerable<ShoppingCartItem> cartItems) : base(discountBase)
        {
            _qualifyingProductName = productName;
            _qualifyingQuantity = quantity;
            _discountQuantity = discount;
            _cartItems = cartItems;
        }


        public override decimal Calculate()
        {
            var previousDiscount = base.Calculate();

            var item = _cartItems
                .FirstOrDefault(c => string.Equals(c.Product.Name, _qualifyingProductName, StringComparison.InvariantCultureIgnoreCase));

            if (item == null || item.Quantity < _qualifyingQuantity)
                return previousDiscount;

            var quantity = item.Quantity / _qualifyingQuantity;
            var newDiscount = quantity * _discountQuantity * item.Product.Price;

            return newDiscount + previousDiscount;
        }
    }
}
