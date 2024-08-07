using Bookstore_Web.Models;
using System.Security.Cryptography;

namespace Bookstore_Web.Data.Repository.IRepository
{
    public interface IOrderDetailsRepository:IRepository<OrderDetails>
    {
        void Update(OrderDetails orderDetails);
        void Save();
    }
}
