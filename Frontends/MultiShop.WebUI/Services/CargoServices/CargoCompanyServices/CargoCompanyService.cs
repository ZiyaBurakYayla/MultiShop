using MultiShop.DtoLayer.CargoDtos.CargoCompanyDtos;

namespace MultiShop.WebUI.Services.CargoServices.CargoCompanyServices
{
    public class CargoCompanyService : ICargoCompanyService
    {
        private readonly HttpClient _httpClient;

        public CargoCompanyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateCargoCompanyAsync(CreateCargoCompanyDto cargoCompanyDto)
        {
            await _httpClient.PostAsJsonAsync<CreateCargoCompanyDto>("CargoCompanies", cargoCompanyDto);
        }

        public async Task DeleteCargoCompanyAsync(int cargoCompanyId)
        {
            await _httpClient.DeleteAsync("CargoCompanies?id=" + cargoCompanyId);
        }

        public async Task<List<ResultCargoCompanyDto>> GetAllCargoCompanyAsync()
        {
            var response = await _httpClient.GetAsync("CargoCompanies");
            var values = await response.Content.ReadFromJsonAsync<List<ResultCargoCompanyDto>>();
            return values;
        }

        public async Task<GetByIdCargoCompanyDto> GetByIdCargoCompanyAsync(int cargoCompanyId)
        {
            var responseMessage = await _httpClient.GetAsync("CargoCompanies/" + cargoCompanyId);
            var values = await responseMessage.Content.ReadFromJsonAsync<GetByIdCargoCompanyDto>();
            return values;
        }

        public async Task UpdateCargoCompanyAsync(UpdateCargoCompanyDto cargoCompanyDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateCargoCompanyDto>("CargoCompanies", cargoCompanyDto);
        }
    }
}
