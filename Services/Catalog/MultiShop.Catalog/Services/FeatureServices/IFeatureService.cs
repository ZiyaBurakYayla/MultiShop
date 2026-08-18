using MultiShop.Catalog.Dtos.FeatureDtos;

namespace MultiShop.Catalog.Services.FeatureServices
{
    public interface IFeatureService
    {
        Task<GetByIdFeatureDto> GetFeatureByIdAsync(string featureId);
        Task<List<ResultFeatureDto>> GetAllFeatureAsync();
        Task CreateFeatureAsync(CreateFeatureDto featureDto);
        Task UpdateFeatureAsync(UpdateFeatureDto featureDto);
        Task DeleteFeatureAsync(string featureId);
    }
}
