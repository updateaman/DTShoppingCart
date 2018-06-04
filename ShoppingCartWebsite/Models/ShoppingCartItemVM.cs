using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingCartDAL.Models;

namespace ShoppingCartWebsite.Models
{
    public class ShoppingCartItemVM
    {
        public IEnumerable<ShoppingCartItem> ShoppingCartItems { get; set; }
        public decimal Total { get; set; }
    }
}
