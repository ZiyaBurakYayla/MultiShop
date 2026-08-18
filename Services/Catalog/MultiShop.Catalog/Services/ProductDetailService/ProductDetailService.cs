using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductDetailDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductDetailServices
{
    public class ProductDetailService : IProductDetailService
    {
        private readonly IMongoCollection<ProductDetail> _productDetailCollection;
        private readonly IMapper _mapper;
        public ProductDetailService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _productDetailCollection = database.GetCollection<ProductDetail>(_databaseSettings.ProductDetailCollectionName);
            _mapper = mapper;
        }

        public async Task CreateProductDetailAsync(CreateProductDetailDto productDetailDto)
        {
            var value = _mapper.Map<ProductDetail>(productDetailDto);
            await _productDetailCollection.InsertOneAsync(value);
        }

        public async Task DeleteProductDetailAsync(string productDetailId)
        {
            await _productDetailCollection.DeleteOneAsync(z => z.ProductDetailId == productDetailId);
        }

        public async Task<List<ResultProductDetailDto>> GetAllProductDetailsAsync()
        {
            var values = await _productDetailCollection.Find(productDetail => true).ToListAsync();
            var result = _mapper.Map<List<ResultProductDetailDto>>(values);
            return result;
        }

        public async Task<GetByIdProductDetailDto> GetByProductIdProductDetailAsync(string productId)
        {
            var value = await _productDetailCollection.Find(x => x.ProductId == productId).FirstOrDefaultAsync();
            var result = _mapper.Map<GetByIdProductDetailDto>(value);
            return result;
        }

        public async Task<GetByIdProductDetailDto> GetProductDetailByIdAsync(string productDetailId)
        {
            var value = await _productDetailCollection.Find(x => x.ProductDetailId == productDetailId).FirstOrDefaultAsync();
            var result = _mapper.Map<GetByIdProductDetailDto>(value);
            return result;
        }

        public async Task UpdateProductDetailAsync(UpdateProductDetailDto productDetailDto)
        {
            var value = _mapper.Map<ProductDetail>(productDetailDto);
            await _productDetailCollection.ReplaceOneAsync(x => x.ProductDetailId == productDetailDto.ProductDetailId, value);
        }
    }
}
