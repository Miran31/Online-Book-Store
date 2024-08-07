using Bookstore_Web.Models;
using System.Security.Cryptography;

namespace Bookstore_Web.Data.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        void Update(ShoppingCart shoppingCart);
        void Save();
    }
}
