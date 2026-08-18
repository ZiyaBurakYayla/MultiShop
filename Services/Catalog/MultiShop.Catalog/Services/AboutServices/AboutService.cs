using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.AboutDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.AboutServices
{
    public class AboutService : IAboutService
    {
        private readonly IMongoCollection<About> _aboutCollection;
        private readonly IMapper _mapper;
        public AboutService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _aboutCollection = database.GetCollection<About>(_databaseSettings.AboutCollectionName);
            _mapper = mapper;
        }

        public async Task CreateAboutAsync(CreateAboutDto aboutDto)
        {
            var value = _mapper.Map<About>(aboutDto);
            await _aboutCollection.InsertOneAsync(value);
        }

        public async Task DeleteAboutAsync(string aboutId)
        {
            await _aboutCollection.DeleteOneAsync(z => z.AboutId == aboutId);
        }

        public async Task<List<ResultAboutDto>> GetAllAboutAsync()
        {
            var values = await _aboutCollection.Find(about => true).ToListAsync();
            var result = _mapper.Map<List<ResultAboutDto>>(values);
            return result;
        }

        public async Task<GetByIdAboutDto> GetAboutByIdAsync(string aboutId)
        {
            var value = await _aboutCollection.Find(x => x.AboutId == aboutId).FirstOrDefaultAsync();
            var result = _mapper.Map<GetByIdAboutDto>(value);
            return result;
        }

        public async Task UpdateAboutAsync(UpdateAboutDto aboutDto)
        {
            var value = _mapper.Map<About>(aboutDto);
            await _aboutCollection.ReplaceOneAsync(x => x.AboutId == aboutDto.AboutId, value);
        }
    }
}
