using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;

namespace MultiShop.WebUI.Services.CatalogServices.FeatureServices
{
    public class FeatureService : IFeatureService
    {
        private readonly HttpClient _httpClient;

        public FeatureService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateFeatureAsync(CreateFeatureDto featureDto)
        {
            await _httpClient.PostAsJsonAsync<CreateFeatureDto>("features", featureDto);
        }

        public async Task DeleteFeatureAsync(string id)
        {
            await _httpClient.DeleteAsync("features?id=" + id);
        }

        public async Task<List<ResultFeatureDto>> GetAllFeaturesAsync()
        {
            var response = await _httpClient.GetAsync("features");
            var values = await response.Content.ReadFromJsonAsync<List<ResultFeatureDto>>();
            return values;
        }

        public async Task<UpdateFeatureDto> GetFeatureByIdAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("features/" + id);
            var values = await responseMessage.Content.ReadFromJsonAsync<UpdateFeatureDto>();
            return values;
        }

        public async Task UpdateFeatureAsync(UpdateFeatureDto featureDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateFeatureDto>("features", featureDto);
        }
    }
}
