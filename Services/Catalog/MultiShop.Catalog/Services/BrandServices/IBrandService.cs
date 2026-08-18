using MultiShop.Catalog.Dtos.BrandDtos;

namespace MultiShop.Catalog.Services.BrandServices
{
    public interface IBrandService
    {
        Task<GetByIdBrandDto> GetBrandByIdAsync(string brandId);
        Task<List<ResultBrandDto>> GetAllBrandAsync();
        Task CreateBrandAsync(CreateBrandDto brandDto);
        Task UpdateBrandAsync(UpdateBrandDto brandDto);
        Task DeleteBrandAsync(string brandId);
    }
}
