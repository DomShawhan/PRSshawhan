using Microsoft.EntityFrameworkCore;
using PRSshawhan.Models.EF;

namespace PRSshawhan.Models
{
    public class PrsDbContext : DbContext
    {
        // DbSet properties to access data from SQL
        public DbSet<User> Users { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<LineItem> LineItems { get; set; }

        public PrsDbContext(DbContextOptions<PrsDbContext> options) : base(options)
        {
        }
    }
}
