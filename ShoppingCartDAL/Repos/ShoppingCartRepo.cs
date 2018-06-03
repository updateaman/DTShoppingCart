using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingCartDAL.EF;
using ShoppingCartDAL.Models;

namespace ShoppingCartDAL.Repos
{
    public class ShoppingCartRepo : IShoppingCartRepo
    {
        private readonly ShoppingCartDbContext _db;
        public ShoppingCartRepo(ShoppingCartDbContext db)
        {
            _db = db;
        }

        //public ShoppingCartRepo(DbContextOptions<ShoppingCartDbContext> options)
        //{
        //    _db = new ShoppingCartDbContext(options);
        //}

        public async Task<List<ShoppingCartItem>> GetAllAsync()
        {
            return await _db.ShoppingCartItems.ToListAsync();
        }
    }
}
