using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartServices
{
    public abstract class DiscountBaseDecorator
    {
        private readonly DiscountBaseDecorator _base;
        public DiscountBaseDecorator(DiscountBaseDecorator discountBase)
        {
            _base = discountBase;
        }

        public virtual decimal Calculate()
        {
            return _base?.Calculate() ?? 0;
        }
    }
}
