using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using test.dataAccess.Data;
using test.dataAccess.Repository.IRepository;

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
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet.Where(filter);
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
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
