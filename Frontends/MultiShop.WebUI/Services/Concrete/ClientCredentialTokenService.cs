using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Settings;

namespace MultiShop.WebUI.Services.Concrete
{
    public class ClientCredentialTokenService : IClientCredentialTokenService
    {
        private readonly ServiceApiSettings _serviceApiSettings;
        private readonly HttpClient _httpClient;
        private readonly IClientAccessTokenCache _clientAccessTokenCache;
        private readonly ClientSettings _clientSettinggs;

        public ClientCredentialTokenService(IOptions<ServiceApiSettings> serviceApiSettings, 
            HttpClient httpClient, IClientAccessTokenCache clientAccessTokenCache, IOptions<ClientSettings> clientSettinggs)
        {
            _serviceApiSettings = serviceApiSettings.Value;
            _httpClient = httpClient;
            _clientAccessTokenCache = clientAccessTokenCache;
            _clientSettinggs = clientSettinggs.Value;
        }

        public async Task<string> GetTokenAsync()
        {
            var currentToken = await _clientAccessTokenCache.GetAsync("multishoptoken");
            if (currentToken != null)
            {
                return currentToken.AccessToken;
            }
            var discoveryEndpoint = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _serviceApiSettings.IdentityServerUrl,
                Policy = new DiscoveryPolicy
                {
                    RequireHttps = false
                }
            });
            var clientCredentialTokenRequest = new ClientCredentialsTokenRequest
            {
                Address = discoveryEndpoint.TokenEndpoint,
                ClientId = _clientSettinggs.MultiShopVisitorClient.ClientId,
                ClientSecret = _clientSettinggs.MultiShopVisitorClient.ClientSecret
            };

            var newToken = await _httpClient.RequestClientCredentialsTokenAsync(clientCredentialTokenRequest);
            await _clientAccessTokenCache.SetAsync("multishoptoken", newToken.AccessToken, newToken.ExpiresIn);
            return newToken.AccessToken;
        }
    }
}
