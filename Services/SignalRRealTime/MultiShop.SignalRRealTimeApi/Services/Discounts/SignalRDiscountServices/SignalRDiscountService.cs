namespace MultiShop.SignalRRealTimeApi.Services.Discounts.SignalRDiscountServices
{
    public class SignalRDiscountService : ISignalRDiscountService
    {
        private readonly HttpClient _httpClient;

        public SignalRDiscountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetDiscountCouponCountAsync()
        {
            var response = await _httpClient.GetAsync("Statistics/GetDiscountCouponCount");
            var values = await response.Content.ReadFromJsonAsync<int>();
            return values;
        }
    }
}
