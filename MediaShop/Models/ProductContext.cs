using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace MediaShop.Models
{
    class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["ProductContext"].ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .ToTable("Product");
            modelBuilder.Entity<Product>(b =>
            {
                b.HasKey(e => e.id);
                b.Property(e => e.id).ValueGeneratedOnAdd();
            });
        }
    }
}
