using System.Security.Claims;
using System.Threading.Tasks;
using GeekShopping.IdentityServer.Configuration;
using GeekShopping.IdentityServer.Infrastructure.Model;
using IdentityModel;
using Microsoft.AspNetCore.Identity;

namespace GeekShopping.IdentityServer.Infrastructure.Context
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<ApplicationUser> _user;
        private readonly RoleManager<IdentityRole> _role;

        public DbInitializer(UserManager<ApplicationUser> user,
            RoleManager<IdentityRole> role)
        {
            _user = user;
            _role = role;
        }
        
        public async Task Initialize()
        {
            if(_role.FindByNameAsync(IdentityConfiguration.ADMIN).Result != null) return;

            await CreateAdminUser();
            await CreateClientUser();
        }

        private async Task CreateClientUser()
        {
            await _role.CreateAsync(new IdentityRole(IdentityConfiguration.CLIENT));
            
            ApplicationUser client = new ApplicationUser()
            {
                UserName = "jonathan-client",
                Email = "client@client.com",
                EmailConfirmed = true,
                PhoneNumber = "+55 (92) 12345-6789",
                FistName = "Jonathan",
                LastName = "Client"
            };

            await _user.CreateAsync(client, "@Jonathan123");
            await _user.AddToRoleAsync(client, IdentityConfiguration.CLIENT);

            await _user.AddClaimsAsync(client, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{client.FistName} {client.LastName}"),
                new Claim(JwtClaimTypes.GivenName, client.FistName),
                new Claim(JwtClaimTypes.FamilyName, client.LastName),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.ADMIN)
            });
        }
        
        private async Task CreateAdminUser()
        {
            await _role.CreateAsync(new IdentityRole(IdentityConfiguration.ADMIN));
            
            ApplicationUser admin = new ApplicationUser()
            {
                UserName = "jonathan-admin",
                Email = "admin@admin.com",
                EmailConfirmed = true,
                PhoneNumber = "+55 (92) 12345-6789",
                FistName = "Jonathan",
                LastName = "Admin"
            };

            await _user.CreateAsync(admin, "@Jonathan123");
            await _user.AddToRoleAsync(admin, IdentityConfiguration.ADMIN);

            await _user.AddClaimsAsync(admin, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{admin.FistName} {admin.LastName}"),
                new Claim(JwtClaimTypes.GivenName, admin.FistName),
                new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.ADMIN)
            });
        }
    }
}