using MultiShop.DtoLayer.CargoDtos.CargoDetailDtos;

namespace MultiShop.WebUI.Services.CargoServices.CargoDetailServices
{
    public interface ICargoDetailService
    {
        Task<List<ResultCargoDetailDto>> GetAllCargoDetailsAsync();
        Task<List<ResultCargoDetailDto>> GetMyCargosAsync();
        Task<ResultCargoDetailDto> GetByBarcodeAsync(string barcode);
        Task<List<ResultCargoDetailDto>> GetByCompanyIdAsync(int companyId);
        Task CreateCargoDetailAsync(CreateCargoDetailDto createCargoDetailDto);
        Task UpdateStatusAsync(UpdateCargoStatusDto updateCargoStatusDto);
    }
}
