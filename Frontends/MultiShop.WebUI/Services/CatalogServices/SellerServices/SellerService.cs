using MultiShop.DtoLayer.CatalogDtos.SellerDtos;

namespace MultiShop.WebUI.Services.CatalogServices.SellerServices
{
    public class SellerService : ISellerService
    {
        private readonly HttpClient _httpClient;

        public SellerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateSellerAsync(CreateSellerDto sellerDto)
        {
            await _httpClient.PostAsJsonAsync<CreateSellerDto>("sellers", sellerDto);
        }

        public async Task DeleteSellerAsync(string id)
        {
            await _httpClient.DeleteAsync("sellers?id=" + id);
        }

        public async Task<List<ResultSellerDto>> GetAllSellersAsync()
        {
            var response = await _httpClient.GetAsync("sellers");
            var values = await response.Content.ReadFromJsonAsync<List<ResultSellerDto>>();
            return values;
        }

        public async Task<List<ResultSellerDto>> GetSellersByStatusAsync(string status)
        {
            var response = await _httpClient.GetAsync("sellers/SellerListByStatus/" + status);
            var values = await response.Content.ReadFromJsonAsync<List<ResultSellerDto>>();
            return values;
        }

        public async Task<UpdateSellerDto> GetSellerByIdAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("sellers/" + id);
            var values = await responseMessage.Content.ReadFromJsonAsync<UpdateSellerDto>();
            return values;
        }

        public async Task UpdateSellerAsync(UpdateSellerDto sellerDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateSellerDto>("sellers", sellerDto);
        }
    }
}
