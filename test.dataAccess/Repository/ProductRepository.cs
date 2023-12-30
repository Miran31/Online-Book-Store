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
            var productFromDb = _db.Products.FirstOrDefault(u=>u.Id == product.Id);
            if (productFromDb != null)
            {
                productFromDb.ISBN=product.ISBN;
                productFromDb.ListPrice = product.ListPrice;
                productFromDb.Price = product.Price;
                productFromDb.Price100 = product.Price100;
                productFromDb.Price50 = product.Price50;
                productFromDb.Author = product.Author;
                productFromDb.Description = product.Description;
                productFromDb.CategoryID = product.CategoryID;
                productFromDb.Title = product.Title;
                if(product.imageUrl != null)
                {
                    productFromDb.imageUrl = product.imageUrl;
                }
            }
        }
    }
}
