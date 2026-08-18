using MultiShop.DtoLayer.RapidApiDtos.CatalogProductDtos;
using MultiShop.WebUI.Models.Requests;

namespace MultiShop.WebUI.Services.RapidApiServices.CatalogProductServices
{
    public class ProductSearchService : IProductSearchService
    {
        private readonly HttpClient _httpClient;

        public ProductSearchService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductSearchDto>> SearchAsync(ProductSearchRequest request)
        {
            var response = await _httpClient.GetAsync(
                $"Products/search?productName={Uri.EscapeDataString(request.ProductName)}");
            response.EnsureSuccessStatusCode();
            var values = await response.Content.ReadFromJsonAsync<List<ProductSearchDto>>();
            return values ?? new List<ProductSearchDto>();
        }
    }
}
