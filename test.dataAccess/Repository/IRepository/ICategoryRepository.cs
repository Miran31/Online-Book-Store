using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test.Models;

namespace test.dataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void update(Category obj);
        void save();
    }
}
