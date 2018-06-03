using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoppingCartDAL.EF;
using ShoppingCartDAL.Models;

namespace ShoppingCartDAL.Repos
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShoppingCartDbContext _db;
        public ProductRepository(ShoppingCartDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _db.Products.ToListAsync();
        }
    }
}
