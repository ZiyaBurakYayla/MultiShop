using MultiShop.DtoLayer.CatalogDtos.BrandDtos;

namespace MultiShop.WebUI.Services.CatalogServices.BrandServices
{
    public class BrandService : IBrandService
    {
        private readonly HttpClient _httpClient;

        public BrandService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateBrandAsync(CreateBrandDto brandDto)
        {
            await _httpClient.PostAsJsonAsync<CreateBrandDto>("brands", brandDto);
        }

        public async Task DeleteBrandAsync(string id)
        {
            await _httpClient.DeleteAsync("brands?id=" + id);
        }

        public async Task<List<ResultBrandDto>> GetAllBrandsAsync()
        {
            var response = await _httpClient.GetAsync("brands");
            var values = await response.Content.ReadFromJsonAsync<List<ResultBrandDto>>();
            return values;
        }

        public async Task<UpdateBrandDto> GetBrandByIdAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("brands/" + id);
            var values = await responseMessage.Content.ReadFromJsonAsync<UpdateBrandDto>();
            return values;
        }

        public async Task UpdateBrandAsync(UpdateBrandDto brandDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateBrandDto>("brands", brandDto);
        }
    }
}
