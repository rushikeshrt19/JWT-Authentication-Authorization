using Custom_JWT_Token.Models;
using Microsoft.EntityFrameworkCore;

namespace Custom_JWT_Token
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Book> Book { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
