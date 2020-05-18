// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
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
                            Name = "role",
                            DisplayName = "Read only access to the calendar"
                        }
                    },

                }
            };
        
        public static IEnumerable<Client> Clients =>
            new Client[] 
            {};
        
    }
}