using MultiShop.DtoLayer.CargoDtos.CargoCompanyDtos;

namespace MultiShop.WebUI.Services.CargoServices.CargoCompanyServices
{
    public interface ICargoCompanyService
    {
        Task<List<ResultCargoCompanyDto>> GetAllCargoCompanyAsync();
        Task CreateCargoCompanyAsync(CreateCargoCompanyDto cargoCompanyDto);
        Task UpdateCargoCompanyAsync(UpdateCargoCompanyDto cargoCompanyDto);
        Task DeleteCargoCompanyAsync(int cargoCompanyId);
        Task<GetByIdCargoCompanyDto> GetByIdCargoCompanyAsync(int cargoCompanyId);
    }
}
