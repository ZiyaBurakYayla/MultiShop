namespace MultiShop.WebUI.Services.IdentityServices.RoleServices
{
    public class RoleService : IRoleService
    {
        private readonly HttpClient _httpClient;

        public RoleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AssignRoleAsync(string userId, string role)
        {
            await _httpClient.PostAsync($"api/roles?userId={userId}&role={role}", null);
        }
    }
}
