namespace MultiShop.SignalRRealTimeApi.Services.Catalogs.SignalRProductServices
{
    public class SignalRProductService : ISignalRProductService
    {
        private readonly HttpClient _httpClient;

        public SignalRProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> GetLastProductNameAsync()
        {
            var response = await _httpClient.GetAsync("Statistics/GetLastProductName");
            var values = await response.Content.ReadAsStringAsync();
            return values;
        }

        public async Task<string> GetMaxPriceProductNameAsync()
        {
            var response = await _httpClient.GetAsync("Statistics/GetMaxPriceProductName");
            var values = await response.Content.ReadAsStringAsync();
            return values;
        }

        public async Task<string> GetMinPriceProductNameAsync()
        {
            var response = await _httpClient.GetAsync("Statistics/GetMinPriceProductName");
            var values = await response.Content.ReadAsStringAsync();
            return values;
        }

        public async Task<decimal> GetProductAvgPriceAsync()
        {
            var response = await _httpClient.GetAsync("Statistics/GetProductAvgPrice");
            var values = await response.Content.ReadFromJsonAsync<decimal>();
            return values;
        }

        public async Task<long> GetProductCountAsync()
        {
            var response = await _httpClient.GetAsync("Statistics/GetProductCount");
            var values = await response.Content.ReadFromJsonAsync<long>();
            return values;
        }
    }
}
