using AutoMapper;
using GeekShopping.ProductAPI.Infrastructure.Models;

namespace GeekShopping.ProductAPI.Infrastructure.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Domain.ValueObjects.Product, Product>()
                .ReverseMap();
        }
    }
}