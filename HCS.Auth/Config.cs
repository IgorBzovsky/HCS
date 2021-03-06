﻿using HCS.Core.Domain;
using IdentityServer4;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Identity;
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
                new IdentityResources.Profile(),
                new IdentityResources.Email()
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
                    RequireConsent = false,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = { "http://localhost:5000/auth-callback" },
                    PostLogoutRedirectUris = { "http://localhost:5000/" },
                    AllowedCorsOrigins = { "http://localhost:5000" },
                
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "hcsApi",
                        "role"
                    },
                
                }
            };
        }

        public class IdentityProfileService : IProfileService
        {

            private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;
            private readonly UserManager<ApplicationUser> _userManager;

            public IdentityProfileService(IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, UserManager<ApplicationUser> userManager)
            {
                _claimsFactory = claimsFactory;
                _userManager = userManager;
            }

            public async Task GetProfileDataAsync(ProfileDataRequestContext context)
            {
                var sub = context.Subject.GetSubjectId();
                var user = await _userManager.FindByIdAsync(sub);
                if (user == null)
                {
                    throw new ArgumentException("");
                }

                var principal = await _claimsFactory.CreateAsync(user);
                var claims = principal.Claims.ToList();

                //Add more claims like this
                /*var userClaims = await _userManager.GetClaimsAsync(user);
                if(userClaims.Any(c => c.Type == "role" && c.Value == "admin"))
                    claims.Add(new System.Security.Claims.Claim("role", "admin"));*/

                context.IssuedClaims = claims;
            }

            public async Task IsActiveAsync(IsActiveContext context)
            {
                var sub = context.Subject.GetSubjectId();
                var user = await _userManager.FindByIdAsync(sub);
                context.IsActive = user != null;
            }
        }
    }
}
