namespace MultiShop.SignalRRealTimeApi.Services.Messages.SignalRMessageServices
{
    public class SignalRMessageService : ISignalRMessageService
    {
        private readonly HttpClient _httpClient;

        public SignalRMessageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetTotalMessageCountAsync()
        {
            var response = await _httpClient.GetAsync("Statistics/GetTotalMessageCount");
            var values = await response.Content.ReadFromJsonAsync<int>();
            return values;
        }

        public async Task<int> GetTotalMessageCountByReceiverIdAsync(string id)
        {
            var response = await _httpClient.GetAsync("Statistics/GetTotalMessageCountByReceiverId?id=" + id);
            var values = await response.Content.ReadFromJsonAsync<int>();
            return values;
        }
    }
}
