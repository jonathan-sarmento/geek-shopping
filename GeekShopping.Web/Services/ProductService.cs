using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Principal;
using System.Threading.Tasks;
using GeekShopping.Web.Models;
using GeekShopping.Web.Services.Abstractions;
using GeekShopping.Web.Utils;

namespace GeekShopping.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _client;
        private const string BasePath = "api/v1/product";
        
        public ProductService(HttpClient client, IGeekShoppingPrincipal principal)
        {
            _client = client;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", principal.Token());
        }
        
        public async Task<IEnumerable<ProductModel>> FindAllProductsAsync()
        {
            var response = await _client.GetAsync(BasePath);
            return await response.ReadContentAs<List<ProductModel>>();
        }

        public async Task<ProductModel> FindProductByIdAsync(long id)
        {
            var response = await _client.GetAsync($"{BasePath}/{id}");
            return await response.ReadContentAs<ProductModel>();
        }

        public async Task<long> CreateProductAsync(ProductModel model)
        {
            var response = await _client.PostAsJson(BasePath, model);
            return response.IsSuccessStatusCode
                ? await response.ReadContentAs<long>()
                : throw new Exception("Something went wrong when calling API");
        }

        public async Task<ProductModel> UpdateProductAsync(ProductModel model)
        {
            var response = await _client.PutAsJsonAsync(BasePath, model);
            return response.IsSuccessStatusCode
                ? await response.ReadContentAs<ProductModel>()
                : throw new Exception("Something went wrong when calling API");
        }

        public async Task<bool> DeleteProductByIdAsync(long id)
        {
            var response = await _client.DeleteAsync($"{BasePath}/{id}");
            return response.IsSuccessStatusCode
                ? await response.ReadContentAs<bool>()
                : throw new Exception("Something went wrong when calling API");
        }
    }
}