using MultiShop.DtoLayer.CatalogDtos.ProductDetailDtos;

namespace MultiShop.WebUI.Services.CatalogServices.ProductDetailServices
{
    public interface IProductDetailService
    {
        Task<GetByIdProductDetailDto> GetByProductIdProductDetailAsync(string productId);
        Task UpdateProductDetailAsync(UpdateProductDetailDto productDetailDto);
    }
}
