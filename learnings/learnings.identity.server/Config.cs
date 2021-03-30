// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace learnings.identity.server
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };

        public static IEnumerable<ApiResource> ApiResources =>
         new ApiResource[]
         {
              
         };
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            { new ApiScope("api1","My API")};

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
              new Client
                {
                    ClientId = "client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api1" }
                },
              new Client
                {
            ClientId = "angular",
            RequireClientSecret=false,
            ClientName="Angular App",
            AllowedGrantTypes = GrantTypes.Code,
            AllowAccessTokensViaBrowser = true,
            // where to redirect to after login
            RedirectUris = { "http://localhost:4200", "http://localhost:4200/silent-callback.html"  },
            // where to redirect to after logout
            PostLogoutRedirectUris = { "http://localhost:4200/signout-callback-oidc" },
            RequireConsent = false,
            AllowedScopes = new List<string>
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.Email
            }
           }
              };
    }
}