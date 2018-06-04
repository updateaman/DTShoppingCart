using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<ShoppingCartItem>> GetAllAsync()
        {
            return await _db.ShoppingCartItems.Include(s => s.Product).ToListAsync();
        }

        public async Task<ShoppingCartItem> GetByProductIdAsync(int productId)
        {
            return await _db.ShoppingCartItems.Where(s => s.ProductId == productId).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(ShoppingCartItem item)
        {
            _db.ShoppingCartItems.Update(item);
            await _db.SaveChangesAsync();
        }

        public async Task InsertAsync(ShoppingCartItem item)
        {
            await _db.ShoppingCartItems.AddAsync(item);
            await _db.SaveChangesAsync();
        }
    }
}
