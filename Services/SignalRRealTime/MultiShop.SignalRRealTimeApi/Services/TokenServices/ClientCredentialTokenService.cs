using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using MultiShop.SignalRRealTimeApi.Settings;

namespace MultiShop.SignalRRealTimeApi.Services.TokenServices
{
    public class ClientCredentialTokenService : IClientCredentialTokenService
    {
        private readonly ServiceApiSettings _serviceApiSettings;
        private readonly HttpClient _httpClient;
        private readonly IClientAccessTokenCache _clientAccessTokenCache;
        private readonly ClientSettings _clientSettings;

        public ClientCredentialTokenService(IOptions<ServiceApiSettings> serviceApiSettings,
            HttpClient httpClient, IClientAccessTokenCache clientAccessTokenCache, IOptions<ClientSettings> clientSettings)
        {
            _serviceApiSettings = serviceApiSettings.Value;
            _httpClient = httpClient;
            _clientAccessTokenCache = clientAccessTokenCache;
            _clientSettings = clientSettings.Value;
        }

        public async Task<string> GetTokenAsync()
        {
            var currentToken = await _clientAccessTokenCache.GetAsync("signalrtoken");
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
                ClientId = _clientSettings.MultiShopSignalRClient.ClientId,
                ClientSecret = _clientSettings.MultiShopSignalRClient.ClientSecret
            };

            var newToken = await _httpClient.RequestClientCredentialsTokenAsync(clientCredentialTokenRequest);
            await _clientAccessTokenCache.SetAsync("signalrtoken", newToken.AccessToken, newToken.ExpiresIn);
            return newToken.AccessToken;
        }
    }
}
