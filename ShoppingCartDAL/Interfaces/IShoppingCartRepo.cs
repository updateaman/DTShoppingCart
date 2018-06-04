using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingCartDAL.Models;

namespace ShoppingCartDAL.Repos
{
    public interface IShoppingCartRepo
    {
        Task<List<ShoppingCartItem>> GetAllAsync();
        Task<ShoppingCartItem> GetByProductIdAsync(int productId);
        Task UpdateAsync(ShoppingCartItem item);
        Task InsertAsync(ShoppingCartItem item);
    }
}