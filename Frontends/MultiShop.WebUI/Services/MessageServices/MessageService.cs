using MultiShop.DtoLayer.MessageDtos;

namespace MultiShop.WebUI.Services.MessageServices
{
    public class MessageService : IMessageService
    {
        private readonly HttpClient _httpClient;

        public MessageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultInboxMessageDto>> GetInboxMessageAsync(string id)
        {
            var response = await _httpClient.GetAsync("UserMessage/GetMessageInbox?id=" + id);
            var values = await response.Content.ReadFromJsonAsync<List<ResultInboxMessageDto>>();
            return values;
        }

        public async Task<List<ResultSendboxMessageDto>> GetSendboxMessageAsync(string id)
        {
            var response = await _httpClient.GetAsync("UserMessage/GetMessageSendbox?id=" + id);
            var values = await response.Content.ReadFromJsonAsync<List<ResultSendboxMessageDto>>();
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
