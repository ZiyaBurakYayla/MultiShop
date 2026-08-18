using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;

namespace MultiShop.WebUI.Services.CatalogServices.FeatureServices
{
    public interface IFeatureService
    {
        Task<UpdateFeatureDto> GetFeatureByIdAsync(string id);
        Task<List<ResultFeatureDto>> GetAllFeaturesAsync();
        Task CreateFeatureAsync(CreateFeatureDto featureDto);
        Task UpdateFeatureAsync(UpdateFeatureDto featureDto);
        Task DeleteFeatureAsync(string id);
    }
}
