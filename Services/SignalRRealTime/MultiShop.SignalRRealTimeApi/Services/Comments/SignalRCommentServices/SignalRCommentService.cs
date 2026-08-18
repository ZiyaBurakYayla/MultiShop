namespace MultiShop.SignalRRealTimeApi.Services.Comments.SignalRCommentServices
{
    public class SignalRCommentService : ISignalRCommentService
    {
        private readonly HttpClient _httpClient; 

        public SignalRCommentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetCommentCountAsync()
        {
            var response = await _httpClient.GetAsync("Statistics/GetTotalCommentCount");
            var values = await response.Content.ReadFromJsonAsync<int>();
            return values;
        }

        public async Task<int> GetActiveCommentCountAsync()
        {
            var response = await _httpClient.GetAsync("Statistics/GetActiveCommentCount");
            var values = await response.Content.ReadFromJsonAsync<int>();
            return values;
        }

        public async Task<int> GetPassiveCommentCountAsync()
        {
            var response = await _httpClient.GetAsync("Statistics/GetPassiveCommentCount");
            var values = await response.Content.ReadFromJsonAsync<int>();
            return values;
        }
    }
}

