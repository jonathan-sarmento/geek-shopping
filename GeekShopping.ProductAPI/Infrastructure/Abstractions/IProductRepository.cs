using GeekShopping.ProductAPI.Domain.ValueObjects;

namespace GeekShopping.ProductAPI.Infrastructure.Abstractions
{
    public interface IProductRepository : IRepository<Product, Models.Product, long>
    {
    }
}