using System.Net.Http.Json;
using MultiShop.DtoLayer.OrderDtos.OrderDetailDtos;

namespace MultiShop.WebUI.Services.OrderServices.OrderDetailServices
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly HttpClient _httpClient;

        public OrderDetailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> CreateOrderDetail(CreateOrderDetailDto createOrderDetailDto)
        {
            var response = await _httpClient.PostAsJsonAsync("orderdetails", createOrderDetailDto);
            if (response.IsSuccessStatusCode)
            {
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            return "(" + (int)response.StatusCode + ") " + content;
        }

        public async Task<List<ResultOrderDetailDto>> GetOrderDetailsByOrderingIdAsync(int orderingId)
        {
            var response = await _httpClient.GetAsync("orderdetails");
            if (!response.IsSuccessStatusCode)
            {
                return new List<ResultOrderDetailDto>();
            }
            var values = await response.Content.ReadFromJsonAsync<List<ResultOrderDetailDto>>();
            if (values == null)
            {
                return new List<ResultOrderDetailDto>();
            }
            return values.Where(x => x.OrderingId == orderingId).ToList();
        }

        public async Task<List<ResultOrderDetailBySellerDto>> GetOrderDetailsBySellerIdAsync(string sellerId)
        {
            var response = await _httpClient.GetAsync("orderdetails/OrderDetailListBySellerId/" + sellerId);
            if (!response.IsSuccessStatusCode)
            {
                return new List<ResultOrderDetailBySellerDto>();
            }
            var values = await response.Content.ReadFromJsonAsync<List<ResultOrderDetailBySellerDto>>();
            return values;
        }
    }
}
