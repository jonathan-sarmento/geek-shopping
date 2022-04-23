using System.Collections.Generic;
using System.Threading.Tasks;
using GeekShopping.ProductAPI.Domain.Base;
using GeekShopping.ProductAPI.Domain.ValueObjects;

namespace GeekShopping.ProductAPI.Infrastructure.Abstractions
{
    public interface IProductRepository : IRepository<Models.Product, long>
    {
    }
}