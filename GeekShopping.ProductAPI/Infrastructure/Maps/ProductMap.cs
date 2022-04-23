using GeekShopping.ProductAPI.Domain;
using GeekShopping.ProductAPI.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekShopping.ProductAPI.Infrastructure.Maps
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(nameof(Product).ToLower());
            
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName(nameof(Product.Id).ToLower());

            builder.Property(x => x.Name)
                .HasColumnName(nameof(Product.Name).ToLower())
                .IsRequired()
                .HasMaxLength(150);
            
            builder.Property(x => x.Price)
                .HasColumnName(nameof(Product.Price).ToLower())
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnName(nameof(Product.Description).ToLower())
                .HasMaxLength(500);
            
            builder.Property(x => x.CategoryName)
                .HasColumnName(nameof(Product.CategoryName).ToLower())
                .HasMaxLength(50);
            
            builder.Property(x => x.ImageUrl)
                .HasColumnName(nameof(Product.ImageUrl).ToLower())
                .HasMaxLength(300);
        }
    }
}