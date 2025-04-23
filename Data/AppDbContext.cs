using examensarbeteBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace examensarbeteBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Users = Set<User>();
        }

        public DbSet<User> Users { get; set; }
    }
}
