using System.Net.Http.Json;

namespace MultiShop.WebUI.Services.StatisticServices.CargoStatisticServices
{
    public class CargoStatisticService : ICargoStatisticService
    {
        private readonly HttpClient _httpClient;

        public CargoStatisticService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetTotalCargoCustomerCountAsync()
        {
            var response = await _httpClient.GetAsync("Statistics/GetTotalCargoCustomerCount");
            var values = await response.Content.ReadFromJsonAsync<int>();
            return values;
        }
    }
}
