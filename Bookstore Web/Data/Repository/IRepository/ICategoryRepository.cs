using Bookstore_Web.Models;
using System.Security.Cryptography;

namespace Bookstore_Web.Data.Repository.IRepository
{
    public interface ICategoryRepository:IRepository<Category>
    {
        void Update(Category category);
        void Save();
    }
}
