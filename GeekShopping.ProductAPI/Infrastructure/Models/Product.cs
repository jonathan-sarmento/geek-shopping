using GeekShopping.ProductAPI.Domain.Base;

namespace GeekShopping.ProductAPI.Domain
{
    public class Product : SimpleId<long>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
    }
}