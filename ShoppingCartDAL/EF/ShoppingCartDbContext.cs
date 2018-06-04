using Microsoft.EntityFrameworkCore;
using ShoppingCartDAL.Models;

namespace ShoppingCartDAL.EF
{
    public class ShoppingCartDbContext : DbContext
    {
        public ShoppingCartDbContext() { }
        public ShoppingCartDbContext(DbContextOptions options)
            : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connString = "Server=(localdb)\\mssqllocaldb;Database=ShoppingCartDb;Trusted_Connection=True;MultipleActiveResultSets=true;";
                optionsBuilder.UseSqlServer(connString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
