using System;
using GeekShopping.IdentityServer.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GeekShopping.IdentityServer.Infrastructure.IoC
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
    }
}