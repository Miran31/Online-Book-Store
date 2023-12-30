using System.Linq.Expressions;
namespace test.dataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string? includeProperty = null);
        T Get(Expression<Func<T, bool>> filter, string? includeProperty = null); 
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
        
    }
}
