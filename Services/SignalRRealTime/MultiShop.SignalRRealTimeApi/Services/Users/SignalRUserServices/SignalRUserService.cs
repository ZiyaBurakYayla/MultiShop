namespace MultiShop.SignalRRealTimeApi.Services.Users.SignalRUserServices
{
    public class SignalRUserService : ISignalRUserService
    {
        private readonly HttpClient _httpClient;

        public SignalRUserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetUserCountAsync()
        {
            var response = await _httpClient.GetAsync("Statistics/GetUserCount");
            var values = await response.Content.ReadFromJsonAsync<int>();
            return values;
        }
    }
}
