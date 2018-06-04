using System;
using System.Collections.Generic;
using System.Text;
using ShoppingCartServices.Decorators;
using ShoppingCartServices.Interfaces;

namespace ShoppingCartServices
{
    public class DiscountFactory: IDiscountFactory
    {
        public DiscountBaseDecorator GetDiscounts()
        {
            DiscountBaseDecorator baseDiscount = null;
            baseDiscount = new QuantityDiscountDecorator(baseDiscount, "Milk", 4, 1);
            baseDiscount = new QuantityADiscountBDecorator(baseDiscount, "Butter", "Bread", 2, 0.50M);
            return baseDiscount;
        }
    }
}
