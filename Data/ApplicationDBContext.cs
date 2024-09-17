using InformationSystems.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InformationSystems.Server.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        // DbSet is a collection of stock entities that can be queried
        public DbSet<Models.Stock> Stocks { get; set; }
        public DbSet<Models.Comment> Comments { get; set; }
        public DbSet<Models.Portfolio> Portfolios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure the relationship between the stock and users
            modelBuilder.Entity<Models.Portfolio>(x => x.HasKey(p => new {p.AppUserId, p.StockId}));
            modelBuilder.Entity<Models.Portfolio>()
                .HasOne(p => p.User)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p => p.AppUserId);
            modelBuilder.Entity<Models.Portfolio>()
                .HasOne(p => p.Stock)
                .WithMany(s => s.Portfolios)
                .HasForeignKey(p => p.StockId);
            // Add roles to the database
            List<IdentityRole> roles = new List<IdentityRole> { 
                new IdentityRole{ Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole{ Name = "User", NormalizedName = "USER" }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }

    }
}
