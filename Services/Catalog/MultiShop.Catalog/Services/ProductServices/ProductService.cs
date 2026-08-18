using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;
        public ProductService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Product>(_databaseSettings.ProductCollectionName);
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task CreateProductAsync(CreateProductDto productDto)
        {
            var value = _mapper.Map<Product>(productDto);
            await _productCollection.InsertOneAsync(value);
        }

        public async Task DeleteProductAsync(string productId)
        {
            await _productCollection.DeleteOneAsync(z => z.ProductId == productId);
        }

        public async Task<List<ResultProductDto>> GetAllProductsAsync()
        {
            var values = await _productCollection.Find(product => true).ToListAsync();
            var result = _mapper.Map<List<ResultProductDto>>(values);
            return result;
        }

        public async Task<GetByIdProductDto> GetProductByIdAsync(string productId)
        {
            var value = await _productCollection.Find(x => x.ProductId == productId).FirstOrDefaultAsync();
            var result = _mapper.Map<GetByIdProductDto>(value);
            return result;
        }

        public async Task<List<ResultProductWithCategoryDto>> GetProductWithCategoryAsync()
        {
            var values = await _productCollection.Find(x => true).ToListAsync();
            foreach (var item in values)
            {
                item.Category = await _categoryCollection.Find<Category>(x => x.CategoryId == item.CategoryId).FirstOrDefaultAsync();
            }
            return _mapper.Map<List<ResultProductWithCategoryDto>>(values);
        }

        public async Task<List<ResultProductWithCategoryDto>> GetProductWithCategoryByCategoryIdAsync(string categoryId)
        {
            var values = await _productCollection.Find(x => x.CategoryId == categoryId).ToListAsync();
            foreach (var item in values)
            {
                item.Category = await _categoryCollection.Find<Category>(x => x.CategoryId == item.CategoryId).FirstOrDefaultAsync();
            }
            return _mapper.Map<List<ResultProductWithCategoryDto>>(values);
        }

        public async Task UpdateProductAsync(UpdateProductDto productDto)
        {
            var value = _mapper.Map<Product>(productDto);
            await _productCollection.ReplaceOneAsync(x => x.ProductId == productDto.ProductId, value);
        }
        public async Task<List<ResultProductDto>> GetProductsBySellerIdAsync(string sellerId)
        {
            var values = await _productCollection.Find(x => x.SellerId == sellerId).ToListAsync();
            var result = _mapper.Map<List<ResultProductDto>>(values);
            return result;
        }

        public async Task<bool> ReduceStockAsync(string sku, int quantity)
        {
            var product = await _productCollection.Find(x => x.Variants.Any(v => v.Sku == sku)).FirstOrDefaultAsync();
            if (product == null)
            {
                return false;
            }
            foreach (var variant in product.Variants)
            {
                if (variant.Sku == sku)
                {
                    if (variant.Stock < quantity)
                    {
                        return false;
                    }
                    variant.Stock = variant.Stock - quantity;
                }
            }
            await _productCollection.ReplaceOneAsync(x => x.ProductId == product.ProductId, product);
            return true;
        }
    }
}
