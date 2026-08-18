
namespace MultiShop.SignalRRealTimeApi.Services.Catalogs.SignalRBrandServices
{
    public class SignalRBrandService : ISignalRBrandService
    {
        private readonly HttpClient _httpClient;

        public SignalRBrandService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<long> GetBrandCountAsync()
        {
            var response = await _httpClient.GetAsync("Statistics/GetBrandCount");
            var values = await response.Content.ReadFromJsonAsync<long>();
            return values;
        }

        public async Task<string> GetLastBrandNameAsync()
        {
            var response = await _httpClient.GetAsync("Statistics/GetLastBrandName");
            var values = await response.Content.ReadAsStringAsync();
            return values;
        }
    }
}
