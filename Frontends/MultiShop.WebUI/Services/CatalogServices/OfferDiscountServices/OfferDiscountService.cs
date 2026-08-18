using MultiShop.DtoLayer.CatalogDtos.OfferDiscountDtos;

namespace MultiShop.WebUI.Services.CatalogServices.OfferDiscountServices
{
    public class OfferDiscountService : IOfferDiscountService
    {
        private readonly HttpClient _httpClient;

        public OfferDiscountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateOfferDiscountAsync(CreateOfferDiscountDto offerDiscountDto)
        {
            await _httpClient.PostAsJsonAsync<CreateOfferDiscountDto>("offerdiscounts", offerDiscountDto);
        }

        public async Task DeleteOfferDiscountAsync(string id)
        {
            await _httpClient.DeleteAsync("offerdiscounts?id=" + id);
        }

        public async Task<List<ResultOfferDiscountDto>> GetAllOfferDiscountsAsync()
        {
            var response = await _httpClient.GetAsync("offerdiscounts");
            var values = await response.Content.ReadFromJsonAsync<List<ResultOfferDiscountDto>>();
            return values;
        }

        public async Task<UpdateOfferDiscountDto> GetOfferDiscountByIdAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("offerdiscounts/" + id);
            var values = await responseMessage.Content.ReadFromJsonAsync<UpdateOfferDiscountDto>();
            return values;
        }

        public async Task UpdateOfferDiscountAsync(UpdateOfferDiscountDto offerDiscountDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateOfferDiscountDto>("offerdiscounts", offerDiscountDto);
        }
    }
}
