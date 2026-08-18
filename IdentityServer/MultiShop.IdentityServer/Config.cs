// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace MultiShop.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("ResourceCatalog")
            {
                Scopes={"CatalogFullPermission","CatalogReadPermission"}
            },
            new ApiResource("ResourceDiscount")
            {
                Scopes={"DiscountFullPermission"}
            },
            new ApiResource("ResourceOrder")
            {
                Scopes={ "OrderFullPermission" }
            },
            new ApiResource("ResourceCargo")
            {
                Scopes={ "CargoFullPermission" }
            },
            new ApiResource("ResourceBasket")
            {
                Scopes = { "BasketFullPermission" }
            },           
            new ApiResource("ResourceComment")
            {
                Scopes = { "CommentFullPermission" }
            },
            new ApiResource("ResourcePayment")
            {
                Scopes = { "PaymentFullPermission" }
            },
            new ApiResource("ResourceImage")
            {
                Scopes = { "ImageFullPermission" }
            },
            new ApiResource("ResourceMessage")
            {
                Scopes = { "MessageFullPermission" }
            },
            new ApiResource("ResourceOcelot")
            {
                Scopes = { "OcelotFullPermission" }
            },
            new ApiResource("ResourceRapidApi")
            {
                Scopes = { "RapidApiFullPermission" }
            },
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };
        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile(),
            new IdentityResource("roles", "Kullanıcı Rolleri", new List<string> { "role" })
        };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new ApiScope("CatalogFullPermission","Full authority for catalog operations"),
            new ApiScope("CatalogReadPermission","Reading authority for catalog operations"),
            new ApiScope("DiscountFullPermission","Full authority for discount operations"),
            new ApiScope("OrderFullPermission","Full authority for order operations"),
            new ApiScope("CargoFullPermission","Full authority for cargo operations"),
            new ApiScope("BasketFullPermission","Full authority for basket operations"),
            new ApiScope("CommentFullPermission","Full authority for comment operations"),
            new ApiScope("PaymentFullPermission","Full authority for payment operations"),
            new ApiScope("ImageFullPermission","Full authority for image operations"),
            new ApiScope("MessageFullPermission","Full authority for message operations"),
            new ApiScope("OcelotFullPermission","Full authority for ocelot operations"),
            new ApiScope("RapidApiFullPermission","Full authority for rapidapi operations"),
            new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<Client> Clients(IConfiguration configuration) => new Client[]
        {
            //visitor
            new Client
            {
                ClientId="MultiShopVisitorId",
                ClientName = "Multi Shop Visitor User",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets ={new Secret(configuration["ClientSecret"].Sha256())},
                AllowedScopes = {"CatalogReadPermission" ,"CatalogFullPermission"
                    , "OcelotFullPermission" , "CommentFullPermission","ImageFullPermission","RapidApiFullPermission"}
            },

            //SignalR
            new Client
            {
                ClientId = "MultiShopSignalRId",
                ClientName = "Multi Shop SignalR Service",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = {new Secret(configuration["ClientSecret"].Sha256())},
                AllowedScopes = {"CatalogReadPermission","OcelotFullPermission","CommentFullPermission",
                    "MessageFullPermission","OrderFullPermission","CargoFullPermission","DiscountFullPermission",
                    IdentityServerConstants.LocalApi.ScopeName}
            },

            //Manager
            new Client
            {
                ClientId = "MultiShopManagerId",
                ClientName = "Multi Shop Manager User",
                AllowedGrantTypes= GrantTypes.ResourceOwnerPassword,
                ClientSecrets = {new Secret(configuration["ClientSecret"].Sha256())},
                AllowedScopes = {"CatalogReadPermission","CatalogFullPermission", "BasketFullPermission","OcelotFullPermission",
                   "CommentFullPermission","PaymentFullPermission","ImageFullPermission","DiscountFullPermission",
                   "OrderFullPermission","CargoFullPermission","MessageFullPermission","RapidApiFullPermission",
                    IdentityServerConstants.LocalApi.ScopeName,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "roles" },
                AllowOfflineAccess = true
            }
        };
    }
}