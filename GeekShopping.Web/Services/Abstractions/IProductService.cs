using System.Collections.Generic;
using System.Threading.Tasks;
using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.Abstractions
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> FindAllProductsAsync();
        Task<ProductModel> FindProductByIdAsync(long id);
        Task<long> CreateProductAsync(ProductModel model);
        Task<ProductModel> UpdateProductAsync(ProductModel model);
        Task<bool> DeleteProductByIdAsync(long id);
    }
}