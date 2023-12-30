using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using test.dataAccess.Data;
using test.dataAccess.Repository.IRepository;
using test.Models;

namespace test.dataAccess.Repository
{
    public class Reposiroty<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Reposiroty(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet=_db.Set<T>();
            _db.Products.Include(u => u.Category).Include(u => u.CategoryID);
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter,string? includeProperty=null)
        {
            IQueryable<T> query = dbSet.Where(filter);
            if (!string.IsNullOrEmpty(includeProperty))
            {
                foreach (var i in includeProperty.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(i);
                }
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeProperty = null)
        {
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includeProperty))
            {
                foreach(var i in includeProperty.Split(',',StringSplitOptions.RemoveEmptyEntries)) 
                { 
                    query = query.Include(i);
                }
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
