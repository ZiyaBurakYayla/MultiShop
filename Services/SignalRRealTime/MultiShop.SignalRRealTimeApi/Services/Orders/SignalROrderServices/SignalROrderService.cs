namespace MultiShop.SignalRRealTimeApi.Services.Orders.SignalROrderServices
{
    public class SignalROrderService : ISignalROrderService
    {
        private readonly HttpClient _httpClient;

        public SignalROrderService(HttpClient httpClient)
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
