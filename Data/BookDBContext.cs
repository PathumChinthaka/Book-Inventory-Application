using Book_Inventory_System.Model;
using Microsoft.EntityFrameworkCore;

namespace Book_Inventory_System.Data
{
    public class BookDBContext : DbContext
    {
        public BookDBContext(DbContextOptions<BookDBContext>options):base(options)
        {

        }
        public DbSet<Book> Book { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //config table 1
            modelBuilder.ApplyConfiguration(new BookConfig());
        }
    }
}
