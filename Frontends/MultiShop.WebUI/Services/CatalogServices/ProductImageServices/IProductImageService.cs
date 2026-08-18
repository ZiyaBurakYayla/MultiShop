using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;

namespace MultiShop.WebUI.Services.CatalogServices.ProductImageServices
{
    public interface IProductImageService
    {
        Task<UpdateProductImageDto> GetByProductIdProductImageAsync(string productId);
        Task UpdateProductImageAsync(UpdateProductImageDto productImageDto);
    }
}
