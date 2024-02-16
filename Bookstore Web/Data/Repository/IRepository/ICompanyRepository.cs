using Bookstore_Web.Models;
using System.Security.Cryptography;

namespace Bookstore_Web.Data.Repository.IRepository
{
    public interface ICompanyRepository:IRepository<Company>
    {
        void Update(Company company);
        void Save();
    }
}
