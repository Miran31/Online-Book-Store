using Microsoft.EntityFrameworkCore;
using test.dataAccess.Data;
using test.dataAccess.Repository.IRepository;
using test.Models;

namespace test.dataAccess.Repository
{
    public class ProductRepository : Reposiroty<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Product product)
        {
            _db.Update(product);
        }
    }
}
