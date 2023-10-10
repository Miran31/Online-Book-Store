using Microsoft.EntityFrameworkCore;
using testweb.Models;

namespace testweb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { ID=1, Name="Shirt", DisplayOrder=1},
                new Category { ID=2, Name="Pant", DisplayOrder=1},
                new Category { ID=3, Name="Tupt", DisplayOrder=1}
                );
        }
    }
}
