using MultiShop.DtoLayer.OrderDtos.OrderingDtos;
using System.Net;

namespace MultiShop.WebUI.Services.OrderServices.OrderingServices
{
    public class OrderingService : IOrderingService
    {
        private readonly HttpClient _httpClient;

        public OrderingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultOrderingByUserIdDto>> GetOrderingByUserId(string userId)
        {
            var response = await _httpClient.GetAsync($"orderings/GetOrderingByUserId?id={userId}");

            if (!response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NoContent)
                return new List<ResultOrderingByUserIdDto>();

            var values = await response.Content.ReadFromJsonAsync<List<ResultOrderingByUserIdDto>>();
            return values ?? new List<ResultOrderingByUserIdDto>();
        }

        public async Task CreateOrdering(CreateOrderingDto createOrderingDto)
        {
            await _httpClient.PostAsJsonAsync("orderings", createOrderingDto);
        }
    }
}
