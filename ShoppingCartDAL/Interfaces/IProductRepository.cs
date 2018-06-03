using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingCartDAL.Models;

namespace ShoppingCartDAL.Repos
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
    }
}