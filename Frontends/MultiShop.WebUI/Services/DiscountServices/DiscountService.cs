using MultiShop.DtoLayer.DiscountDtos;

namespace MultiShop.WebUI.Services.DiscountServices
{
    public class DiscountService : IDiscountService
    {
        private readonly HttpClient _httpClient;

        public DiscountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetDiscountCodeDetailByCodeDto> GetDiscountCode(string code)
        {
            var response = await _httpClient.GetAsync($"discounts/GetCodeDetailByCode/{code}");
            var values = await response.Content.ReadFromJsonAsync<GetDiscountCodeDetailByCodeDto>();
            return values;
        }

        public async Task<int> GetDiscountCouponRate(string code)
        {
            var response = await _httpClient.GetAsync($"discounts/GetDiscountCouponRate?code={code}");
            var values = await response.Content.ReadFromJsonAsync<int>();
            return values;
        }
    }
}
