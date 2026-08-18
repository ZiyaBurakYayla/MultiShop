namespace MultiShop.SignalRRealTimeApi.Services.Catalogs.SignalRProductImageServices
{
    public class SignalRProductImageService : ISignalRProductImageService
    {
        private readonly HttpClient _httpClient;

        public SignalRProductImageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<long> GetProductImageCountAsync()
        {
            var response = await _httpClient.GetAsync("Statistics/GetProductImageCount");
            var values = await response.Content.ReadFromJsonAsync<long>();
            return values;
        }
    }
}
