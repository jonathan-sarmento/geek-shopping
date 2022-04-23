using GeekShopping.ProductAPI.Domain;
using GeekShopping.ProductAPI.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Infrastructure.Context
{
    public class MySqlContext : DbContext
    {
        public MySqlContext()
        { }

        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MySqlContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}