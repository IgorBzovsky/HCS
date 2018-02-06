using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HCS.Auth
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("hcsApi", "HCS Api", new[] { "role" })
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public class Users
        {
            public static List<TestUser> All()
            {
                return new List<TestUser> {
                    new TestUser
                    {
                        SubjectId = "1",
                        Username = "test@domain.com",
                        Password = "password",
                        Claims = new List<Claim>
                        {
                            new Claim("role", "admin"),
                            new Claim("role", "manager")
                        }
                    },
                    new TestUser
                    {
                        SubjectId = "2",
                        Username = "test2@domain.com",
                        Password = "password",
                        Claims = new List<Claim>
                        {
                            new Claim("role", "user")
                        }
                    }
                };
            }
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "hcsClient",
                    ClientName = "HCS Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = { "http://localhost:5000/auth-callback" },
                    PostLogoutRedirectUris = { "http://localhost:5000/" },
                    AllowedCorsOrigins = { "http://localhost:5000" },
                
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "hcsApi"
                    },
                
                }
            };
        }
    }
}
