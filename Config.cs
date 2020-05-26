// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace AccountServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[] 
            {
            new ApiResource
                {
                    Name = "greencode",
 
                    // secret for introspection endpoint
                    ApiSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
 
                    // claims to include in access token
                    UserClaims =
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Role
                    },
 
                    // API has multiple scopes
                    Scopes =
                    {
                        new Scope
                        {
                            Name = "greencode",
                            DisplayName = "Read only access to the calendar"
                        }
                    },

                }
            };
        
        public static IEnumerable<Client> Clients =>
            new Client[] 
            {
                    new Client
                {
                    ClientId = "greencode",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    RequirePkce = true,

                    // where to redirect to after login
                    RedirectUris = { "http://localhost:5002/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "greencode"
                        //"account",
                        //"account.read"
                    },

                    AllowOfflineAccess = true
                }
            };
        
    }
}