using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using test.dataAccess.Data;
using test.dataAccess.Repository.IRepository;
using test.Models;

namespace test.dataAccess.Repository
{
    public class CategoryRepository : Reposiroty<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
