using Bookstore_Web.Data.Repository.IRepository;
using Bookstore_Web.Models;

namespace Bookstore_Web.Data.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;
        public ApplicationUserRepository(ApplicationDbContext applicationDbContext):base(applicationDbContext) 
        {
            _context = applicationDbContext;
        }
    }
}
