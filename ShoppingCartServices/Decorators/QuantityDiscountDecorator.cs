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

        public QuantityDiscountDecorator(DiscountBaseDecorator discountBase,
            string productName,
            int quantity,
            int discount
            ) : base(discountBase)
        {
            _qualifyingProductName = productName;
            _qualifyingQuantity = quantity;
            _discountQuantity = discount;
        }


        public override decimal Calculate(IEnumerable<ShoppingCartItem> cartItems)
        {
            var previousDiscount = base.Calculate(cartItems);

            var item = cartItems
                .FirstOrDefault(c => string.Equals(c.Product.Name, _qualifyingProductName, StringComparison.InvariantCultureIgnoreCase));

            if (item == null || item.Quantity < _qualifyingQuantity)
                return previousDiscount;

            var quantity = item.Quantity / _qualifyingQuantity;
            var newDiscount = quantity * _discountQuantity * item.Product.Price;

            return newDiscount + previousDiscount;
        }
    }
}
