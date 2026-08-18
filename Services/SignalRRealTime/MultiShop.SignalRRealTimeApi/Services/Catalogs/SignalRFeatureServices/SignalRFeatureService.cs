namespace MultiShop.SignalRRealTimeApi.Services.Catalogs.SignalRFeatureServices
{
    public class SignalRFeatureService : ISignalRFeatureService
    {
        private readonly HttpClient _httpClient;

        public SignalRFeatureService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<long> GetFeatureCountAsync()
        {
            var response = await _httpClient.GetAsync("Statistics/GetFeatureCount");
            var values = await response.Content.ReadFromJsonAsync<long>();
            return values;
        }
    }
}
