using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;

namespace GeekShopping.Web.Utils
{
    public interface IGeekShoppingPrincipal : IPrincipal
    {
        ICollection<Claim> Claims { get; set; }

        void SetAuthenticated(IIdentity identity);

        void SetAuthenticated(string name, string type);

        string Token();
    }
}