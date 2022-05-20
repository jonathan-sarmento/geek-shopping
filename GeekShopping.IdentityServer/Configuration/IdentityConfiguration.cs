using System.Collections.Generic;
using System.Linq;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using GeekShopping.IdentityServer.Infrastructure.Context;
using GeekShopping.IdentityServer.Infrastructure.IoC;
using GeekShopping.IdentityServer.Infrastructure.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GeekShopping.IdentityServer.Configuration
{
    public static class IdentityConfiguration
    {
        public const string ADMIN = "Admin";
        public const string CLIENT = "Client";
        public const string CLIENTDEFAULT = "Client";
        private static IConfiguration _configuration;
        
        public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            _configuration = configuration;
            
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<MySqlContext>()
                .AddDefaultTokenProviders();

            var builder = services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                    options.EmitStaticAudienceClaim = true;
                })
                .AddInMemoryIdentityResources(IdentityResources)
                .AddInMemoryApiScopes(ApiScopes)
                .AddInMemoryClients(Clients ?? ClientsDefault)
                .AddAspNetIdentity<ApplicationUser>();
            
            services.AddDbInitializer();

            builder.AddDeveloperSigningCredential();

            return services;
        }

        private static IEnumerable<Client> GetAllClients(IConfiguration configuration)
        {
            var clients = configuration.GetSection("Clients").GetChildren();
            var clientsList = new List<Client>();
            foreach (var clientPath in clients.Select(x => x.Path))
            {
                var client = new Client();
                var secretKeys = new List<string>();
                configuration.GetSection(clientPath).Bind(client);
                configuration.GetSection($"{clientPath}:ClientSecrets").Bind(secretKeys);
                var clientSecrets = new List<Secret>();
                secretKeys.ForEach(x => clientSecrets.Add(new Secret(x.Sha256())));
                client.ClientSecrets = clientSecrets;
                clientsList.Add(client);
            }

            return clientsList;
        }

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>()
            {
                new ApiScope("geek_shopping", "GeekShopping Server"),
                new ApiScope("read", "Read data."),
                new ApiScope("write", "Write data."),
                new ApiScope("delete", "Delete data.")
            };

        public static IEnumerable<Client> Clients => GetAllClients(_configuration);

        public static IEnumerable<Client> ClientsDefault =>
            new List<Client>()
            {
                new Client()
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("z3bhjnAXESoIPLnbJgh573sa4VLCTWXo".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "read", "write", "profile" }
                },
                new Client()
                {
                    ClientId = "geek_shopping",
                    ClientSecrets = { new Secret("EuQldJcMtpOcwRsjFO2Hh2hmzqqIa6KC".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = {"http://localhost:5000/signin-oidc"},
                    PostLogoutRedirectUris = {"http://localhost:5000/signout-callback-oidc"},
                    AllowedScopes = new List<string>()
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Profile,
                        "geek_shopping"
                    }
                }
            };
    }
    
}