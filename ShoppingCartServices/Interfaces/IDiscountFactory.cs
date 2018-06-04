using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCartServices.Interfaces
{
    public interface IDiscountFactory
    {
        DiscountBaseDecorator GetDiscounts();
    }
}
