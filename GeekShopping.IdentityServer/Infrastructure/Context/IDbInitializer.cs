using System.Threading.Tasks;

namespace GeekShopping.IdentityServer.Infrastructure.Context
{
    public interface IDbInitializer
    {
        Task Initialize();
    }
}