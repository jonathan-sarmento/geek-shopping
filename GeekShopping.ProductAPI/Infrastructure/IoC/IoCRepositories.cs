using System;
using GeekShopping.ProductAPI.Infrastructure.Abstractions;
using GeekShopping.ProductAPI.Infrastructure.Context;
using GeekShopping.ProductAPI.Infrastructure.Profiles;
using GeekShopping.ProductAPI.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GeekShopping.ProductAPI.Infrastructure.IoC
{
    public static class IoCRepositories
    {
        private static readonly RepositoriesOptions RepositoriesOptions = new RepositoriesOptions();

        public static IServiceCollection AddDbContext(this IServiceCollection services, Action<RepositoriesOptions> options)
        {
            options.Invoke(RepositoriesOptions);

            return services.AddDbContext<MySqlContext>(dbContextOptions =>
                    dbContextOptions.UseMySql(RepositoriesOptions.ConnectionString,
                        ServerVersion.AutoDetect(RepositoriesOptions.ConnectionString), 
                        mySqlOptions => mySqlOptions.MigrationsAssembly(typeof(MySqlContext).Assembly.GetName().Name)));
        }

        public static IServiceCollection AddRepository(this IServiceCollection services)
            => services.AddScoped<IProductRepository, ProductRepository>();
        
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
            => services.AddAutoMapper(typeof(ProductProfile));
    }
}