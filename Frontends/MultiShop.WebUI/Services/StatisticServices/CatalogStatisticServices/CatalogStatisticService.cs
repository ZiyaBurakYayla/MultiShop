
namespace MultiShop.WebUI.Services.StatisticServices.CatalogStatisticServices
{
    public class CatalogStatisticService : ICatalogStatisticService
    {
        private readonly HttpClient _httpClient;

        public CatalogStatisticService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<long> GetBrandCountAsync()
        {
            var response = await _httpClient.GetAsync("Statistics/GetBrandCount");
            var values = await response.Content.ReadFromJsonAsync<long>();
            return values;
        }

        public async Task<long> GetCategoryCountAsync()
        {
            var response = await _httpClient.GetAsync("Statistics/GetCategoryCount");
            var values = await response.Content.ReadFromJsonAsync<long>();
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

        public async Task<long> GetProductImageCountAsync()
        {
            var response = await _httpClient.GetAsync("Statistics/GetProductImageCount");
            var values = await response.Content.ReadFromJsonAsync<long>();
            return values;
        }

        public async Task<long> GetSpecialOfferCountAsync()
        {
            var response = await _httpClient.GetAsync("Statistics/GetSpecialOfferCount");
            var values = await response.Content.ReadFromJsonAsync<long>();
            return values;
        }

        public async Task<long> GetFeatureCountAsync()
        {
            var response = await _httpClient.GetAsync("Statistics/GetFeatureCount");
            var values = await response.Content.ReadFromJsonAsync<long>();
            return values;
        }

        public async Task<string> GetLastBrandNameAsync()
        {
            var response = await _httpClient.GetAsync("Statistics/GetLastBrandName");
            var values = await response.Content.ReadAsStringAsync();
            return values;
        }

        public async Task<string> GetLastProductNameAsync()
        {
            var response = await _httpClient.GetAsync("Statistics/GetLastProductName");
            var values = await response.Content.ReadAsStringAsync();
            return values;
        }
    }
}
