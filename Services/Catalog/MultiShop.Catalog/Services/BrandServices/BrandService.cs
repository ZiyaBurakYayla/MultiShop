using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.BrandDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.BrandServices
{
    public class BrandService : IBrandService
    {
        private readonly IMongoCollection<Brand> _brandCollection;
        private readonly IMapper _mapper;
        public BrandService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _brandCollection = database.GetCollection<Brand>(_databaseSettings.BrandCollectionName);
            _mapper = mapper;
        }

        public async Task CreateBrandAsync(CreateBrandDto brandDto)
        {
            var value = _mapper.Map<Brand>(brandDto);
            await _brandCollection.InsertOneAsync(value);
        }

        public async Task DeleteBrandAsync(string brandId)
        {
            await _brandCollection.DeleteOneAsync(z => z.BrandId == brandId);
        }

        public async Task<List<ResultBrandDto>> GetAllBrandAsync()
        {
            var values = await _brandCollection.Find(brand => true).ToListAsync();
            var result = _mapper.Map<List<ResultBrandDto>>(values);
            return result;
        }

        public async Task<GetByIdBrandDto> GetBrandByIdAsync(string brandId)
        {
            var value = await _brandCollection.Find(x => x.BrandId == brandId).FirstOrDefaultAsync();
            var result = _mapper.Map<GetByIdBrandDto>(value);
            return result;
        }

        public async Task UpdateBrandAsync(UpdateBrandDto brandDto)
        {
            var value = _mapper.Map<Brand>(brandDto);
            await _brandCollection.ReplaceOneAsync(x => x.BrandId == brandDto.BrandId, value);
        }
    }
}
