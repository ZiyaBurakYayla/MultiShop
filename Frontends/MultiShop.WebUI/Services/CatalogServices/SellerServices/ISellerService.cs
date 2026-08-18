using MultiShop.DtoLayer.CatalogDtos.SellerDtos;

namespace MultiShop.WebUI.Services.CatalogServices.SellerServices
{
    public interface ISellerService
    {
        Task<UpdateSellerDto> GetSellerByIdAsync(string id);
        Task<List<ResultSellerDto>> GetAllSellersAsync();
        Task<List<ResultSellerDto>> GetSellersByStatusAsync(string status);
        Task CreateSellerAsync(CreateSellerDto sellerDto);
        Task UpdateSellerAsync(UpdateSellerDto sellerDto);
        Task DeleteSellerAsync(string id);
    }
}
