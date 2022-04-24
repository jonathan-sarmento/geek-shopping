using AutoMapper;
using GeekShopping.ProductAPI.Infrastructure.Abstractions;
using GeekShopping.ProductAPI.Infrastructure.Context;
using GeekShopping.ProductAPI.Infrastructure.Models;

namespace GeekShopping.ProductAPI.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Domain.ValueObjects.Product, Product, long>, IProductRepository
    {
        public ProductRepository(MySqlContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}