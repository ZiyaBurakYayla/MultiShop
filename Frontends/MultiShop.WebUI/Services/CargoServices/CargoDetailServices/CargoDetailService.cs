using MultiShop.DtoLayer.CargoDtos.CargoDetailDtos;

namespace MultiShop.WebUI.Services.CargoServices.CargoDetailServices
{
    public class CargoDetailService : ICargoDetailService
    {
        private readonly HttpClient _httpClient;

        public CargoDetailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResultCargoDetailDto>> GetAllCargoDetailsAsync()
        {
            var response = await _httpClient.GetAsync("cargodetails");
            return await response.Content.ReadFromJsonAsync<List<ResultCargoDetailDto>>();
        }

        public async Task<List<ResultCargoDetailDto>> GetMyCargosAsync()
        {
            var response = await _httpClient.GetAsync("cargodetails/MyCargos");
            if (!response.IsSuccessStatusCode)
            {
                return new List<ResultCargoDetailDto>();
            }
            return await response.Content.ReadFromJsonAsync<List<ResultCargoDetailDto>>();
        }

        public async Task<ResultCargoDetailDto> GetByBarcodeAsync(string barcode)
        {
            var response = await _httpClient.GetAsync("cargodetails/ByBarcode/" + barcode);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return await response.Content.ReadFromJsonAsync<ResultCargoDetailDto>();
        }

        public async Task<List<ResultCargoDetailDto>> GetByCompanyIdAsync(int companyId)
        {
            var response = await _httpClient.GetAsync("cargodetails/ByCompanyId/" + companyId);
            if (!response.IsSuccessStatusCode)
            {
                return new List<ResultCargoDetailDto>();
            }
            return await response.Content.ReadFromJsonAsync<List<ResultCargoDetailDto>>();
        }

        public async Task CreateCargoDetailAsync(CreateCargoDetailDto createCargoDetailDto)
        {
            await _httpClient.PostAsJsonAsync<CreateCargoDetailDto>("cargodetails", createCargoDetailDto);
        }

        public async Task UpdateStatusAsync(UpdateCargoStatusDto updateCargoStatusDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateCargoStatusDto>("cargodetails/UpdateStatus", updateCargoStatusDto);
        }
    }
}
