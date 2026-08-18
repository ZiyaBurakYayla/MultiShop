using MultiShop.DtoLayer.CatalogDtos.BrandDtos;

namespace MultiShop.WebUI.Services.CatalogServices.BrandServices
{
    public interface IBrandService
    {
        Task<UpdateBrandDto> GetBrandByIdAsync(string id);
        Task<List<ResultBrandDto>> GetAllBrandsAsync();
        Task CreateBrandAsync(CreateBrandDto brandDto);
        Task UpdateBrandAsync(UpdateBrandDto brandDto);
        Task DeleteBrandAsync(string id);
    }
}
