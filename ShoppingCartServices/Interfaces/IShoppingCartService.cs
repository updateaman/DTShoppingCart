using System.Threading.Tasks;
using ShoppingCartDAL.Models;
using ShoppingCartWebsite.Models;

namespace ShoppingCartServices
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartItemVM> GetCartAsync();
        Task AddItemAsyc(int productId);
        Task UpdateItemAsync(ShoppingCartItem item);
    }
}