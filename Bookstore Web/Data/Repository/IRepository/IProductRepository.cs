using Bookstore_Web.Models;

namespace Bookstore_Web.Data.Repository.IRepository
{
    public interface IProductRepository:IRepository<Product>
    {
        void Update(Product product);
        void Save();
    }
}
