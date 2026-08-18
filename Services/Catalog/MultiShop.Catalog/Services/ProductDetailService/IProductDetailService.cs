using MultiShop.Catalog.Dtos.ProductDetailDtos;
using MultiShop.Catalog.Dtos.ProductDtos;

namespace MultiShop.Catalog.Services.ProductDetailServices
{
    public interface IProductDetailService
    {
        Task<GetByIdProductDetailDto> GetProductDetailByIdAsync(string productDetailId);
        Task<List<ResultProductDetailDto>> GetAllProductDetailsAsync();
        Task CreateProductDetailAsync(CreateProductDetailDto productDetailDto);
        Task UpdateProductDetailAsync(UpdateProductDetailDto productDetailDto);
        Task DeleteProductDetailAsync(string productDetailId);
        Task<GetByIdProductDetailDto> GetByProductIdProductDetailAsync(string productId);
    }
}
