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
    }
}
