using MultiShop.Catalog.Dtos.SellerDtos;

namespace MultiShop.Catalog.Services.SellerServices
{
    public interface ISellerService
    {
        Task<GetByIdSellerDto> GetSellerByIdAsync(string sellerId);
        Task<List<ResultSellerDto>> GetAllSellerAsync();
        Task<List<ResultSellerDto>> GetSellersByStatusAsync(string status);
        Task CreateSellerAsync(CreateSellerDto sellerDto);
        Task UpdateSellerAsync(UpdateSellerDto sellerDto);
        Task DeleteSellerAsync(string sellerId);
    }
}
