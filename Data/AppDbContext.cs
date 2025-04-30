using Microsoft.EntityFrameworkCore;
using examensarbeteBackend.Models;
using OfferApi.Models;

namespace examensarbeteBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<OfferRequest> OfferRequests { get; set; }
    }
}