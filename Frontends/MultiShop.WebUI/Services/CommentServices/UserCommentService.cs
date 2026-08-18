using MultiShop.DtoLayer.CommentDtos.UserCommentDtos;

namespace MultiShop.WebUI.Services.CommentServices
{
    public class UserCommentService : IUserCommentService
    {
        private readonly HttpClient _httpClient;

        public UserCommentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultUserCommentDto>> GetMyCommentsAsync()
        {
            var response = await _httpClient.GetAsync("comments/MyComments");
            if (!response.IsSuccessStatusCode)
            {
                return new List<ResultUserCommentDto>();
            }
            var values = await response.Content.ReadFromJsonAsync<List<ResultUserCommentDto>>();
            return values ?? new List<ResultUserCommentDto>();
        }

        public async Task<UpdateUserCommentDto> GetMyCommentByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync("comments/MyComments/" + id);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return await response.Content.ReadFromJsonAsync<UpdateUserCommentDto>();
        }

        public async Task<bool> UpdateMyCommentAsync(UpdateUserCommentDto commentDto)
        {
            var response = await _httpClient.PutAsJsonAsync("comments/UpdateMyComment", commentDto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteMyCommentAsync(int id)
        {
            var response = await _httpClient.DeleteAsync("comments/DeleteMyComment/" + id);
            return response.IsSuccessStatusCode;
        }
    }
}
