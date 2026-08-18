namespace MultiShop.SignalRRealTimeApi.Services.Catalogs.SignalRSpecialOfferServices
{
    public class SignalRSpecialOfferService : ISignalRSpecialOfferService
    {
        private readonly HttpClient _httpClient;

        public SignalRSpecialOfferService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<long> GetSpecialOfferCountAsync()
        {
            var response = await _httpClient.GetAsync("Statistics/GetSpecialOfferCount");
            var values = await response.Content.ReadFromJsonAsync<long>();
            return values;
        }
    }
}
