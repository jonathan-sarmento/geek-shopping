using Microsoft.AspNetCore.Identity;

namespace GeekShopping.IdentityServer.Infrastructure.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FistName { get; set; }
        public string LastName { get; set; }
    }
}