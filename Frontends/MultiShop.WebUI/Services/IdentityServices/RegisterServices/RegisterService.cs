using MultiShop.DtoLayer.IdentityDtos.RegisterDtos;

namespace MultiShop.WebUI.Services.IdentityServices.RegisterServices
{
    public class RegisterService : IRegisterService
    {
        private readonly HttpClient _httpClient;

        public RegisterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> CreateUserAsync(CreateRegisterDto createRegisterDto)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync("api/Registers", createRegisterDto);
            if (responseMessage.IsSuccessStatusCode)
            {
                return null;
            }
            var error = await responseMessage.Content.ReadAsStringAsync();
            return $"Kayıt yapılamadı. ({(int)responseMessage.StatusCode}) {error}";
        }
    }
}
