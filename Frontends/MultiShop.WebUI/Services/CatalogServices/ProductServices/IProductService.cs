using MultiShop.DtoLayer.CatalogDtos.ProductDtos;

namespace MultiShop.WebUI.Services.CatalogServices.ProductServices
{
    public interface IProductService
    {
        Task<UpdateProductDto> GetProductByIdAsync(string productId);
        Task<List<ResultProductDto>> GetAllProductsAsync();
        Task<string> CreateProductAsync(CreateProductDto productDto);
        Task<string> UpdateProductAsync(UpdateProductDto productDto);
        Task DeleteProductAsync(string productId);
        Task<List<ResultProductWithCategoryDto>> GetProductWithCategoryAsync();
        Task<List<ResultProductWithCategoryDto>> GetProductWithCategoryByCategoryIdAsync(string categoryId);
        Task<List<ResultProductDto>> GetProductsBySellerIdAsync(string sellerId);
    }
}
