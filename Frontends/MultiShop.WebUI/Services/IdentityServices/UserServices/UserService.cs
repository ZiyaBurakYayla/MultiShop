using MultiShop.DtoLayer.IdentityDtos.UserDtos;
using MultiShop.WebUI.Models;

namespace MultiShop.WebUI.Services.IdentityServices.UserServices
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserDetailViewModel> GetUserInfo()
        {
            return await _httpClient.GetFromJsonAsync<UserDetailViewModel>("api/users/getuser");
        }

        public async Task<List<ResultUserDto>> GetAllUserListAsync()
        {
            var response = await _httpClient.GetAsync("api/users/GetAllUserList");
            if (!response.IsSuccessStatusCode)
            {
                return new List<ResultUserDto>();
            }
            return await response.Content.ReadFromJsonAsync<List<ResultUserDto>>();
        }

        public async Task<string> UpdateUserAsync(UpdateUserDto updateUserDto)
        {
            var responseMessage = await _httpClient.PutAsJsonAsync("api/users/updateuser", updateUserDto);
            if (responseMessage.IsSuccessStatusCode)
            {
                return null;
            }
            var error = await responseMessage.Content.ReadAsStringAsync();
            if (string.IsNullOrWhiteSpace(error))
            {
                return $"Profil güncellenemedi. ({(int)responseMessage.StatusCode})";
            }
            return error;
        }
    }
}
