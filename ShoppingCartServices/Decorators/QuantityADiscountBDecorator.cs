using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShoppingCartDAL.Models;

namespace ShoppingCartServices.Decorators
{
    public class QuantityADiscountBDecorator : DiscountBaseDecorator
    {
        private readonly string _productA;
        private readonly string _productB;
        private readonly int _quantity;
        private readonly decimal _discount;
        public QuantityADiscountBDecorator(DiscountBaseDecorator discountBase,
            string productA,
            string productB,
            int quantity,
            decimal discount) : base(discountBase)
        {
            _productA = productA;
            _productB = productB;
            _quantity = quantity;
            _discount = discount;
        }

        public override decimal Calculate(IEnumerable<ShoppingCartItem> cartItems)
        {
            var previousDiscount = base.Calculate(cartItems);

            var itemA = cartItems
                .FirstOrDefault(c => string.Equals(c.Product.Name, _productA, StringComparison.InvariantCultureIgnoreCase));

            var itemB = cartItems
                .FirstOrDefault(c => string.Equals(c.Product.Name, _productB, StringComparison.InvariantCultureIgnoreCase));

            if (itemA == null || itemB == null || itemA.Quantity < _quantity)
                return previousDiscount;


            var quantity = itemA.Quantity / _quantity;
            var newDiscount = quantity * itemB.Product.Price * _discount;

            return newDiscount + previousDiscount;
        }
    }
}
