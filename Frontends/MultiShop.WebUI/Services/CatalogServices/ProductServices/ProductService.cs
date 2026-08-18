using System.Text.Json;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;

namespace MultiShop.WebUI.Services.CatalogServices.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> CreateProductAsync(CreateProductDto productDto)
        {
            var response = await _httpClient.PostAsJsonAsync<CreateProductDto>("products", productDto);
            if (response.IsSuccessStatusCode)
            {
                return null;
            }
            return await response.Content.ReadAsStringAsync();
        }

        public async Task DeleteProductAsync(string productId)
        {
            await _httpClient.DeleteAsync("products?id=" + productId);
        }

        public async Task<List<ResultProductDto>> GetAllProductsAsync()
        {
            var response = await _httpClient.GetAsync("products");
            var values = await response.Content.ReadFromJsonAsync<List<ResultProductDto>>();
            return values;
        }

        public async Task<UpdateProductDto> GetProductByIdAsync(string productId)
        {
            var responseMessage = await _httpClient.GetAsync("products/" + productId);
            var content = await responseMessage.Content.ReadAsStringAsync();
            if (!responseMessage.IsSuccessStatusCode || string.IsNullOrWhiteSpace(content))
            {
                return null;
            }
            var values = JsonSerializer.Deserialize<UpdateProductDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return values;
        }

        public async Task<string> UpdateProductAsync(UpdateProductDto productDto)
        {
            var response = await _httpClient.PutAsJsonAsync<UpdateProductDto>("products", productDto);
            if (response.IsSuccessStatusCode)
            {
                return null;
            }
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<List<ResultProductWithCategoryDto>> GetProductWithCategoryAsync()
        {
            var response = await _httpClient.GetAsync("products/ProductListWithCategory");
            var values = await response.Content.ReadFromJsonAsync<List<ResultProductWithCategoryDto>>();
            return values;
        }

        public async Task<List<ResultProductWithCategoryDto>> GetProductWithCategoryByCategoryIdAsync(string categoryId)
        {
            var response = await _httpClient.GetAsync("products/ProductListWithCategoryByCategoryId/"+categoryId);
            var values = await response.Content.ReadFromJsonAsync<List<ResultProductWithCategoryDto>>();
            return values;
        }

        public async Task<List<ResultProductDto>> GetProductsBySellerIdAsync(string sellerId)
        {
            var response = await _httpClient.GetAsync("products/ProductListBySellerId/" + sellerId);
            var values = await response.Content.ReadFromJsonAsync<List<ResultProductDto>>();
            return values;
        }
    }
}
