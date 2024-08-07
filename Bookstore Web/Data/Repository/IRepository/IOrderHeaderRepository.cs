using Bookstore_Web.Models;
using System.Security.Cryptography;

namespace Bookstore_Web.Data.Repository.IRepository
{
    public interface IOrderHeaderRepository:IRepository<OrderHeader>
    {
        void Update(OrderHeader orderHeader);
        void Save();
    }
}
