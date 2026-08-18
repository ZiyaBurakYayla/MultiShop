using MultiShop.DtoLayer.CargoDtos.CargoOperationDtos;

namespace MultiShop.WebUI.Services.CargoServices.CargoOperationServices
{
    public class CargoOperationService : ICargoOperationService
    {
        private readonly HttpClient _httpClient;

        public CargoOperationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultCargoOperationDto>> GetByBarcodeAsync(string barcode)
        {
            var response = await _httpClient.GetAsync("cargooperations/ByBarcode/" + barcode);
            if (!response.IsSuccessStatusCode)
            {
                return new List<ResultCargoOperationDto>();
            }
            return await response.Content.ReadFromJsonAsync<List<ResultCargoOperationDto>>();
        }
    }
}
