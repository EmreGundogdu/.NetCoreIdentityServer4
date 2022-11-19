using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.Models;
using ApiResource = IdentityServer4.Models.ApiResource;
using ApiScope = IdentityServer4.Models.ApiScope;
using Client = IdentityServer4.Models.Client;
using IdentityResource = IdentityServer4.Models.IdentityResource;
using Secret = IdentityServer4.Models.Secret;

namespace IdentityServer
{
    public class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources => new[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource
            {
                Name = "role",
                UserClaims = new List<string>{"role"}
            }
        };
        public static IEnumerable<ApiScope> ApiScopes => new[]
        {
            new ApiScope("CoffeeShopAPI.read"),
            new ApiScope("CoffeeShopAPI.write")
        };
        public static IEnumerable<ApiResource> ApiResources => new[]
        {
            new ApiResource("CoffeeAPI")
            {
                Scopes = new List<string>{"CoffeeAPI.read","CoffeeAPI.write"},
                ApiSecrets = new List<Secret>{new Secret ( "ScopeSecret".Sha256()) },
                UserClaims =  new List<string>{"role"}
            }
        };
        public static IEnumerable<Client> Clients => new[]
        {
            new Client()
            {
                //credential flow client
                ClientId = "m2m.client",
                ClientName = "Client Credentials Client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = {new Secret("ClientSecret".Sha256())},
                AllowedScopes = {"CoffeeAPI.read","CoffeeAPI.write"}
            },
            new Client()
            {
                //interactive client using code flow + pkce
                ClientId = "interactive",
                ClientName = "Client Credentials Client",
                ClientSecrets = {new Secret("ClientSecret1".Sha256())},
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = {"https://localhost:5444/signin-oidc"},
                FrontChannelLogoutUri = "https://localhost:5444/signout-callback-oidc",
                PostLogoutRedirectUris = {"https://localhost:5444/signout-callback-oidc"},
                AllowOfflineAccess =true, 
                AllowedScopes = {"openid","profile","CoffeeAPI.read"},
                RequirePkce = true,
                RequireConsent = true,
                AllowPlainTextPkce = false
            },
        };
    }
}
