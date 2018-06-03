﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingCartDAL.Models;

namespace ShoppingCartDAL.Repos
{
    public interface IShoppingCartRepo
    {
        Task<List<ShoppingCartItem>> GetAllAsync();
    }
}