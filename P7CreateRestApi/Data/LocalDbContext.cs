using P7CreateRestApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace P7CreateRestApi.Data

{
    public class LocalDbContext : DbContext
    {
        public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options) { }

        public DbSet<BidList>? BidLists { get; set; } // Assurez-vous que la classe BidList correspond à votre table BidLists
        public DbSet<CurvePoint>? CurvePointss { get; set; }
        public DbSet<Rating>? Rating { get; set; }
        public DbSet<RuleName>? RuleNames { get; set; }
        public DbSet<Trade>? Trades { get; set; }
        public DbSet<User>? User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}

