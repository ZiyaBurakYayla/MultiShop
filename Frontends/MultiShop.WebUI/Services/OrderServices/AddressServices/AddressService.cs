using MultiShop.DtoLayer.OrderDtos.AddressDtos;

namespace MultiShop.WebUI.Services.OrderServices.AddressServices
{
    public class AddressService : IAddressService
    {
        private readonly HttpClient _httpClient;

        public AddressService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateAddressAsync(CreateAddressDto addressDto)
        {
            await _httpClient.PostAsJsonAsync<CreateAddressDto>("addresses", addressDto);
        }
    }
}
