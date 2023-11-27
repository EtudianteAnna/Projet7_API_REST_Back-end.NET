namespace P7CreateRestApi.Data
{ 
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Domain;

    public class CustomIdentityDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public CustomIdentityDbContext(DbContextOptions<CustomIdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedRoles(builder);
        }

        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData
            (
                new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                new IdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" },
                new IdentityRole() { Name = "RH", ConcurrencyStamp = "3", NormalizedName = "RH" }
            );
        }

        public DbSet<User> Users { get; set; }
         public DbSet<BidList>? BidLists { get; set; } // Assurez-vous que la classe BidList correspond à votre table BidLists
        public DbSet<CurvePoint>? CurvePointss { get; set; }
        public DbSet<Rating>? Rating { get; set; }
        public DbSet<RuleName>? RuleNames { get; set; }
        public DbSet<Trade>? Trades { get; set; }
        
    }
}
