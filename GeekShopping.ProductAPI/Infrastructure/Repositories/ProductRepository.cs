using GeekShopping.ProductAPI.Infrastructure.Abstractions;
using GeekShopping.ProductAPI.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product, long>, IProductRepository
    {
        public ProductRepository(DbContext context) : base(context)
        {
        }
    }
}