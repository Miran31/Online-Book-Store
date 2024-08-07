using Bookstore_Web.Data.Repository.IRepository;
using Bookstore_Web.Models;

namespace Bookstore_Web.Data.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly ApplicationDbContext _context;
        public ShoppingCartRepository(ApplicationDbContext applicationDbContext):base(applicationDbContext) 
        {
            _context = applicationDbContext;
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(ShoppingCart shoppingCart)
        {
            _context.Update(shoppingCart); 
        }
    }
}
