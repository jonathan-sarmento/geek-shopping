using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace GeekShopping.Web.Utils
{
    public class GeekShoppingPrincipal : IGeekShoppingPrincipal
    {
        public GeekShoppingPrincipal()
        {
            Identity ??= new GenericIdentity(string.Empty);
            Claims ??= new List<Claim>();
            Roles ??= new List<string>();
        }

        private IEnumerable<string> Roles { get; set; }

        public bool IsInRole(string role)
            => Roles.Contains(role);

        public IIdentity Identity { get; private set; }

        public ICollection<Claim> Claims { get; set; }

        public void SetAuthenticated(string name, string type)
            => SetAuthenticated(new GenericIdentity(name));

        public void SetAuthenticated(IIdentity identity)
        {
            Identity = identity;
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Roles = claimsIdentity?.Claims.Where(x => x.Type == claimsIdentity.RoleClaimType).Select(x => x.Value);
        }

        public bool TryGetClaim(string name, out string value)
        {
            var claim = Claims.SingleOrDefault(x => x.Type.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            value = claim?.Value;
            return value != default;
        }

        public string Token() 
            => TryGetClaim("access_token", out var token) ? token : string.Empty;
    }
}