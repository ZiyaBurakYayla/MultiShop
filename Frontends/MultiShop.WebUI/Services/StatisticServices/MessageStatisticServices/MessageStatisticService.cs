
namespace MultiShop.WebUI.Services.StatisticServices.MessageStatisticServices
{
    public class MessageStatisticService : IMessageStatisticService
    {
        private readonly HttpClient _httpClient;

        public MessageStatisticService(HttpClient httpClient)
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
            var response = await _httpClient.GetAsync("UserMessage/GetTotalMessageCountByReceiverId?id=" + id);
            var values = await response.Content.ReadFromJsonAsync<int>();
            return values;
        }
    }
}
