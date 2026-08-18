namespace MultiShop.SignalRRealTimeApi.Services.Catalogs.SignalRCategoryServices
{
    public class SignalRCategoryService : ISignalRCategoryService
    {
        private readonly HttpClient _httpClient;

        public SignalRCategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<long> GetCategoryCountAsync()
        {
            var response = await _httpClient.GetAsync("Statistics/GetCategoryCount");
            var values = await response.Content.ReadFromJsonAsync<long>();
            return values;
        }
    }
}
