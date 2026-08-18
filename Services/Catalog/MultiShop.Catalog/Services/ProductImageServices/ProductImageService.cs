using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductImageDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductImageServices
{
    public class ProductImageService : IProductImageService
    {
        private readonly IMongoCollection<ProductImage> _productImageCollection;
        private readonly IMapper _mapper;
        public ProductImageService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _productImageCollection = database.GetCollection<ProductImage>(_databaseSettings.ProductImageCollectionName);
            _mapper = mapper;
        }

        public async Task CreateProductImageAsync(CreateProductImageDto productImageDto)
        {
            var value = _mapper.Map<ProductImage>(productImageDto);
            await _productImageCollection.InsertOneAsync(value);
        }

        public async Task DeleteProductImageAsync(string productImageId)
        {
            await _productImageCollection.DeleteOneAsync(z => z.ProductImagesId == productImageId);
        }

        public async Task<List<ResultProductImageDto>> GetAllProductImagesAsync()
        {
            var values = await _productImageCollection.Find(productImage => true).ToListAsync();
            var result = _mapper.Map<List<ResultProductImageDto>>(values);
            return result;
        }

        public async Task<GetByIdProductImageDto> GetProductImageByIdAsync(string productImageId)
        {
            var value = await _productImageCollection.Find(x => x.ProductImagesId == productImageId).FirstOrDefaultAsync();
            var result = _mapper.Map<GetByIdProductImageDto>(value);
            return result;
        }

        public async Task UpdateProductImageAsync(UpdateProductImageDto productImageDto)
        {
            var value = _mapper.Map<ProductImage>(productImageDto);
            await _productImageCollection.ReplaceOneAsync(x => x.ProductImagesId == productImageDto.ProductImagesId, value);
        }

        public async Task<GetByIdProductImageDto> GetByProductIdProductImageAsync(string id)
        {
            var value = await _productImageCollection.Find(x => x.ProductId == id).FirstOrDefaultAsync();
            var result = _mapper.Map<GetByIdProductImageDto>(value);
            return result;
        }
    }
}
