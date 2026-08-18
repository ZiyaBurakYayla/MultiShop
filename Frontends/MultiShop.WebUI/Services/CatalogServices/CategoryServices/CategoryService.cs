using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;

namespace MultiShop.WebUI.Services.CatalogServices.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateCategoryAsync(CreateCategoryDto categoryDto)
        {
            await _httpClient.PostAsJsonAsync<CreateCategoryDto>("categories", categoryDto);
        }

        public async Task DeleteCategoryAsync(string categoryId)
        {
            await _httpClient.DeleteAsync("categories?id=" + categoryId);
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoriesAsync()
        {
            var response = await _httpClient.GetAsync("categories");
            var values = await response.Content.ReadFromJsonAsync<List<ResultCategoryDto>>();
            return values;
        }

        public async Task<UpdateCategoryDto> GetCategoryByIdAsync(string categoryId)
        {
            var responseMessage = await _httpClient.GetAsync("categories/" + categoryId);
            var values = await responseMessage.Content.ReadFromJsonAsync<UpdateCategoryDto>();
            return values;
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto categoryDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateCategoryDto>("categories", categoryDto);
        }
    }
}
