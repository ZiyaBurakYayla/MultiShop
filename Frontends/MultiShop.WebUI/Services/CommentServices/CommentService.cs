using MultiShop.DtoLayer.CommentDtos.UserCommentDtos;
using System.Text.Json;

namespace MultiShop.WebUI.Services.CommentServices
{
    public class CommentService : ICommentService
    {
        private readonly HttpClient _httpClient;

        public CommentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateCommentAsync(CreateUserCommentDto CommentDto)
        {
            await _httpClient.PostAsJsonAsync<CreateUserCommentDto>("comments", CommentDto);
        }

        public async Task DeleteCommentAsync(string id)
        {
            await _httpClient.DeleteAsync("comments?id=" + id);
        }

        public async Task<List<ResultUserCommentDto>> GetAllCommentsAsync()
        {
            var response = await _httpClient.GetAsync("comments");
            var values = await response.Content.ReadFromJsonAsync<List<ResultUserCommentDto>>();
            return values;
        }

        public async Task<UpdateUserCommentDto> GetCommentByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync("comments/" + id);
            var values = await response.Content.ReadFromJsonAsync<UpdateUserCommentDto>();
            return values;
        }

        public async Task<List<ResultUserCommentDto>> CommentsListByProductId(string id)
        {
            var response = await _httpClient.GetAsync("comments/CommentsListByProductId/" + id);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode || string.IsNullOrWhiteSpace(content))
            {
                return new List<ResultUserCommentDto>();
            }
            var values = System.Text.Json.JsonSerializer.Deserialize<List<ResultUserCommentDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return values ?? new List<ResultUserCommentDto>();
        }

        public async Task UpdateCommentAsync(UpdateUserCommentDto CommentDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateUserCommentDto>("comments", CommentDto);
        }

        public async Task ApproveCommentAsync(int id)
        {
            await _httpClient.GetAsync("comments/ApproveComment/" + id);
        }

        public async Task RejectCommentAsync(int id)
        {
            await _httpClient.GetAsync("comments/RejectComment/" + id);
        }
    }
}
