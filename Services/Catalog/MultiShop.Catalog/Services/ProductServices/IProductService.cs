using MultiShop.Catalog.Dtos.ProductDtos;

namespace MultiShop.Catalog.Services.ProductServices
{
    public interface IProductService
    {
        Task<GetByIdProductDto> GetProductByIdAsync(string productId);
        Task<List<ResultProductDto>> GetAllProductsAsync();
        Task CreateProductAsync(CreateProductDto productDto);
        Task UpdateProductAsync(UpdateProductDto productDto);
        Task DeleteProductAsync(string productId);
        Task<List<ResultProductWithCategoryDto>> GetProductWithCategoryAsync();
        Task<List<ResultProductWithCategoryDto>> GetProductWithCategoryByCategoryIdAsync(string categoryId);
        Task<bool> ReduceStockAsync(string sku, int quantity);
        Task<List<ResultProductDto>> GetProductsBySellerIdAsync(string sellerId);

    }
}
