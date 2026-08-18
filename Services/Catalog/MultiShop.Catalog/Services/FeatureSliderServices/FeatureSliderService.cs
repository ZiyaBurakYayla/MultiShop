using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.FeatureSliderDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.FeatureSliderServices
{
    public class FeatureSliderService : IFeatureSliderService
    {
        private readonly IMongoCollection<FeatureSlider> _featureSliderCollection;
        private readonly IMapper _mapper;
        public FeatureSliderService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _featureSliderCollection = database.GetCollection<FeatureSlider>(_databaseSettings.FeatureSliderCollectionName);
            _mapper = mapper;
        }
        public async Task CreateFeatureSliderAsync(CreateFeatureSliderDto FeatureSliderDto)
        {
            var value = _mapper.Map<FeatureSlider>(FeatureSliderDto);
            await _featureSliderCollection.InsertOneAsync(value);
        }

        public async Task DeleteFeatureSliderAsync(string FeatureSliderId)
        {
            await _featureSliderCollection.DeleteOneAsync(z => z.FeatureSliderId == FeatureSliderId);
        }

        public Task FeatureSliderChangeStatusToFalse(string FeatureSliderId)
        {
            throw new NotImplementedException();
        }

        public Task FeatureSliderChangeStatusToTrue(string FeatureSliderId)
        {
            throw new NotImplementedException();
        }
        public async Task<List<ResultFeatureSliderDto>> GetAllFeatureSliderAsync()
        {
            var values = await _featureSliderCollection.Find(FeatureSlider => true).ToListAsync();
            var result = _mapper.Map<List<ResultFeatureSliderDto>>(values);
            return result;
        }

        public async Task<GetByIdFeatureSliderDto> GetFeatureSliderByIdAsync(string FeatureSliderId)
        {
            var value = await _featureSliderCollection.Find(x => x.FeatureSliderId == FeatureSliderId).FirstOrDefaultAsync();
            var result = _mapper.Map<GetByIdFeatureSliderDto>(value);
            return result;
        }

        public async Task UpdateFeatureSliderAsync(UpdateFeatureSliderDto FeatureSliderDto)
        {
            var value = _mapper.Map<FeatureSlider>(FeatureSliderDto);
            await _featureSliderCollection.ReplaceOneAsync(x => x.FeatureSliderId == FeatureSliderDto.FeatureSliderId, value);
        }
    }
}
