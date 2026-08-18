using System.Text.Json;
using MultiShop.DtoLayer.CatalogDtos.ProductDetailDtos;

namespace MultiShop.WebUI.Services.CatalogServices.ProductDetailServices
{
    public class ProductDetailService : IProductDetailService
    {
        private readonly HttpClient _httpClient;

        public ProductDetailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetByIdProductDetailDto> GetByProductIdProductDetailAsync(string productId)
        {
            var responseMessage = await _httpClient.GetAsync("productdetails/GetProductDetailByProductId/" + productId);
            var content = await responseMessage.Content.ReadAsStringAsync();
            if (!responseMessage.IsSuccessStatusCode || string.IsNullOrWhiteSpace(content))
            {
                return null;
            }
            var values = JsonSerializer.Deserialize<GetByIdProductDetailDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return values;
        }

        public async Task UpdateProductDetailAsync(UpdateProductDetailDto productDetailDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateProductDetailDto>("productdetails", productDetailDto);
        }
    }
}
