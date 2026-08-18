using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.FeatureDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.FeatureServices
{
    public class FeatureService : IFeatureService
    {
        private readonly IMongoCollection<Feature> _featureCollection;
        private readonly IMapper _mapper;
        public FeatureService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _featureCollection = database.GetCollection<Feature>(_databaseSettings.FeatureCollectionName);
            _mapper = mapper;
        }

        public async Task CreateFeatureAsync(CreateFeatureDto featureDto)
        {
            var value = _mapper.Map<Feature>(featureDto);
            await _featureCollection.InsertOneAsync(value);
        }

        public async Task DeleteFeatureAsync(string featureId)
        {
            await _featureCollection.DeleteOneAsync(z => z.FeatureId == featureId);
        }

        public async Task<List<ResultFeatureDto>> GetAllFeatureAsync()
        {
            var values = await _featureCollection.Find(feature => true).ToListAsync();
            var result = _mapper.Map<List<ResultFeatureDto>>(values);
            return result;
        }

        public async Task<GetByIdFeatureDto> GetFeatureByIdAsync(string featureId)
        {
            var value = await _featureCollection.Find(x => x.FeatureId == featureId).FirstOrDefaultAsync();
            var result = _mapper.Map<GetByIdFeatureDto>(value);
            return result;
        }

        public async Task UpdateFeatureAsync(UpdateFeatureDto featureDto)
        {
            var value = _mapper.Map<Feature>(featureDto);
            await _featureCollection.ReplaceOneAsync(x => x.FeatureId == featureDto.FeatureId, value);
        }
    }
}
