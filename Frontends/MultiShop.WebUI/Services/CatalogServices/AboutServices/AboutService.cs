using MultiShop.DtoLayer.CatalogDtos.AboutDtos;

namespace MultiShop.WebUI.Services.CatalogServices.AboutServices
{
    public class AboutService : IAboutService
    {
        private readonly HttpClient _httpClient;

        public AboutService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateAboutAsync(CreateAboutDto aboutDto)
        {
            await _httpClient.PostAsJsonAsync<CreateAboutDto>("abouts", aboutDto);
        }

        public async Task DeleteAboutAsync(string id)
        {
            await _httpClient.DeleteAsync("abouts?id=" + id);
        }

        public async Task<List<ResultAboutDto>> GetAllAboutsAsync()
        {
            var response = await _httpClient.GetAsync("abouts");
            var values = await response.Content.ReadFromJsonAsync<List<ResultAboutDto>>();
            return values;
        }

        public async Task<UpdateAboutDto> GetAboutByIdAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("abouts/" + id);
            var values = await responseMessage.Content.ReadFromJsonAsync<UpdateAboutDto>();
            return values;
        }

        public async Task UpdateAboutAsync(UpdateAboutDto aboutDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateAboutDto>("abouts", aboutDto);
        }
    }
}
