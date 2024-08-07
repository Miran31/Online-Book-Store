using Bookstore_Web.Data.Repository.IRepository;
using Bookstore_Web.Models;

namespace Bookstore_Web.Data.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderHeaderRepository(ApplicationDbContext applicationDbContext):base(applicationDbContext) 
        {
            _context = applicationDbContext;
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(OrderHeader orderHeader)
        {
            throw new NotImplementedException();
        }
    }
}
