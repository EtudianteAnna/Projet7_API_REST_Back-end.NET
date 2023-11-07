using Microsoft.EntityFrameworkCore;
using Dot.Net.WebApi.Domain;

namespace P7CreateRestApi.Data
{
    public class LocalDbContext : DbContext
    {
        public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options) { }
        public DbSet<BidList> BidLists { get; set; } // Assurez-vous que la classe BidList correspond à votre table BidLists

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<User> Users { get; set; }
    }
}