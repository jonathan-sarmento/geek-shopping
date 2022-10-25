using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace GeekShopping.Web.Utils
{
    public class IdentityMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var serviceProvider = context.RequestServices;
            
            var geekShoppingPrincipal = serviceProvider.GetService<IGeekShoppingPrincipal>();
            SetForwardHeaders(context, geekShoppingPrincipal);
            await SetAccessToken(context, geekShoppingPrincipal); 
            await next.Invoke(context);
        }
        
        private void SetForwardHeaders(HttpContext context, IGeekShoppingPrincipal geekShoppingPrincipal)
        {
            geekShoppingPrincipal.SetAuthenticated(context.User.Identity);
        }

        private async Task SetAccessToken(HttpContext context, IGeekShoppingPrincipal geekShoppingPrincipal)
        {
            var token = await context.GetTokenAsync("access_token");
            geekShoppingPrincipal.Claims.Add(new Claim("access_token", token ?? string.Empty));
        }
    }
}