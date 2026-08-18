using MultiShop.Catalog.Dtos.ProductImageDtos;

namespace MultiShop.Catalog.Services.ProductImageServices
{
    public interface IProductImageService
    {
        Task<GetByIdProductImageDto> GetProductImageByIdAsync(string productImageId);
        Task<List<ResultProductImageDto>> GetAllProductImagesAsync();
        Task CreateProductImageAsync(CreateProductImageDto productImageDto);
        Task UpdateProductImageAsync(UpdateProductImageDto productImageDto);
        Task DeleteProductImageAsync(string productImageId);
        Task<GetByIdProductImageDto> GetByProductIdProductImageAsync(string id);
    }
}
