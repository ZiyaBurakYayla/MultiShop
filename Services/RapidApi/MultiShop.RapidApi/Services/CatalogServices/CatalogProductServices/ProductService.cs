using MultiShop.RapidApi.Dtos.CatalogDtos.CatalogProductDtos;
using MultiShop.RapidApi.Requests.CatalogRequests.CatalogProductRequests;
using System.Text.Json;

namespace MultiShop.RapidApi.Services.CatalogServices.CatalogProductServices
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ProductService(
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<List<ProductSearchDto>> SearchAsync(
            ProductSearchRequest request)
        {
            var apiKey = _configuration["RapidApi:Key"];

            var url =
                $"https://real-time-product-search.p.rapidapi.com/search" +
                $"?q={Uri.EscapeDataString(request.ProductName)}" +
                $"&country=tr&language=tr";

            using var httpRequest = new HttpRequestMessage(
                HttpMethod.Get,
                url);

            httpRequest.Headers.Add(
                "x-rapidapi-key",
                apiKey);

            httpRequest.Headers.Add(
                "x-rapidapi-host",
                "real-time-product-search.p.rapidapi.com");

            var response = await _httpClient.SendAsync(httpRequest);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            using var document = JsonDocument.Parse(json);

            var products = new List<ProductSearchDto>();

            var productArray =
                document.RootElement
                    .GetProperty("data")
                    .GetProperty("products");

            foreach (var product in productArray.EnumerateArray())
            {
                var dto = new ProductSearchDto
                {
                    ProductTitle = GetString(product, "product_title"),
                    Price = GetString(product, "price"),
                    OriginalPrice = GetString(product, "original_price"),
                    OnSale = GetBool(product, "on_sale"),
                    DiscountPercent = GetString(product, "discount_percent"),
                    ProductPageUrl = GetString(product, "product_page_url"),
                    StoreName = GetString(product, "store_name"),
                    HasMultipleOffers = GetNullableBool(product, "has_multiple_offers"),
                    ProductRating = GetNullableDouble(product, "product_rating"),
                    ProductNumReviews = GetNullableInt(product, "product_num_reviews"),
                    Shipping = GetString(product, "shipping")
                };

                if (product.TryGetProperty("product_photos", out var photos) &&
                    photos.ValueKind == JsonValueKind.Array &&
                    photos.GetArrayLength() > 0)
                {
                    dto.ProductPhoto = photos[0].GetString();
                }

                products.Add(dto);
            }

            return products;
        }

        private static string GetString(
            JsonElement element,
            string propertyName)
        {
            if (element.TryGetProperty(propertyName, out var property) &&
                property.ValueKind != JsonValueKind.Null)
            {
                return property.GetString();
            }

            return null;
        }

        private static bool GetBool(
            JsonElement element,
            string propertyName)
        {
            if (element.TryGetProperty(propertyName, out var property) &&
                property.ValueKind == JsonValueKind.True)
            {
                return true;
            }

            return false;
        }

        private static bool? GetNullableBool(
            JsonElement element,
            string propertyName)
        {
            if (!element.TryGetProperty(propertyName, out var property))
                return null;

            if (property.ValueKind == JsonValueKind.Null)
                return null;

            return property.GetBoolean();
        }

        private static double? GetNullableDouble(
            JsonElement element,
            string propertyName)
        {
            if (!element.TryGetProperty(propertyName, out var property))
                return null;

            if (property.ValueKind == JsonValueKind.Null)
                return null;

            return property.GetDouble();
        }

        private static int? GetNullableInt(
            JsonElement element,
            string propertyName)
        {
            if (!element.TryGetProperty(propertyName, out var property))
                return null;

            if (property.ValueKind == JsonValueKind.Null)
                return null;

            return property.GetInt32();
        }
    }
}