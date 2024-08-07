using Bookstore_Web.Data.Repository.IRepository;
using Bookstore_Web.Models;

namespace Bookstore_Web.Data.Repository
{
    public class OrderDetailsRepository : Repository<OrderDetails>, IOrderDetailsRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderDetailsRepository(ApplicationDbContext applicationDbContext):base(applicationDbContext) 
        {
            _context = applicationDbContext;
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(OrderDetails orderDetails)
        {
            throw new NotImplementedException();
        }
    }
}
