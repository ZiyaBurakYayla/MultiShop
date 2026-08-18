using System.Text.Json;
using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;

namespace MultiShop.WebUI.Services.CatalogServices.ProductImageServices
{
    public class ProductImageService : IProductImageService
    {
        private readonly HttpClient _httpClient;

        public ProductImageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UpdateProductImageDto> GetByProductIdProductImageAsync(string productId)
        {
            var responseMessage = await _httpClient.GetAsync("productimages/ProductImagesByProductId/" + productId);
            var content = await responseMessage.Content.ReadAsStringAsync();
            if (!responseMessage.IsSuccessStatusCode || string.IsNullOrWhiteSpace(content))
            {
                return null;
            }
            var values = JsonSerializer.Deserialize<UpdateProductImageDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return values;
        }
        public async Task UpdateProductImageAsync(UpdateProductImageDto productImageDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateProductImageDto>("productimages", productImageDto);
        }
    }
}
