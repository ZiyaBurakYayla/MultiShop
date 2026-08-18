using MultiShop.DtoLayer.CargoDtos.CargoOperationDtos;

namespace MultiShop.WebUI.Services.CargoServices.CargoOperationServices
{
    public interface ICargoOperationService
    {
        Task<List<ResultCargoOperationDto>> GetByBarcodeAsync(string barcode);
    }
}
