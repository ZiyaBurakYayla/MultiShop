namespace MultiShop.SignalRRealTimeApi.Services.TokenServices
{
    public interface IClientCredentialTokenService
    {
        Task<string> GetTokenAsync();
    }
}
