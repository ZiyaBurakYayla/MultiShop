using System.Net.Http.Json;

namespace MultiShop.WebUI.Services.StatisticServices.OrderStatisticServices
{
    public class OrderStatisticService : IOrderStatisticService
    {
        private readonly HttpClient _httpClient;

        public OrderStatisticService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetTotalOrderCountAsync()
        {
            var response = await _httpClient.GetAsync("Statistics/GetTotalOrderCount");
            var values = await response.Content.ReadFromJsonAsync<int>();
            return values;
        }

        public async Task<int> GetTotalOrderDetailCountAsync()
        {
            var response = await _httpClient.GetAsync("Statistics/GetTotalOrderDetailCount");
            var values = await response.Content.ReadFromJsonAsync<int>();
            return values;
        }
    }
}
