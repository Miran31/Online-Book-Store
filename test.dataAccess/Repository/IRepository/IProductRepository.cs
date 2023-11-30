using test.Models;
namespace test.dataAccess.Repository.IRepository
{
    public interface IProductRepository:IRepository<Product>
    {
        void Update(Product product);
        void Save();
    }
}
