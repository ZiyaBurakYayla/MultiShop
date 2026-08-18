using MultiShop.Catalog.Dtos.FeatureSliderDtos;

namespace MultiShop.Catalog.Services.FeatureSliderServices
{
    public interface IFeatureSliderService
    {
        Task<List<ResultFeatureSliderDto>> GetAllFeatureSliderAsync();
        Task CreateFeatureSliderAsync(CreateFeatureSliderDto FeatureSliderDto);
        Task UpdateFeatureSliderAsync(UpdateFeatureSliderDto FeatureSliderDto);
        Task DeleteFeatureSliderAsync(string FeatureSliderId);
        Task<GetByIdFeatureSliderDto> GetFeatureSliderByIdAsync(string FeatureSliderId);
        Task FeatureSliderChangeStatusToTrue(string FeatureSliderId);
        Task FeatureSliderChangeStatusToFalse(string FeatureSliderId);
    }
}
