using MongoDB.Bson;
using MongoDB.Driver;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;
namespace MultiShop.Catalog.Services.StatisticServices
{
    public class StatisticService : IStatisticService
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMongoCollection<Brand> _brandCollection;
        private readonly IMongoCollection<ProductImage> _productImageCollection;
        private readonly IMongoCollection<SpecialOffer> _specialOfferCollection;
        private readonly IMongoCollection<Feature> _featureCollection;

        public StatisticService(IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Product>(_databaseSettings.ProductCollectionName);
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
            _brandCollection = database.GetCollection<Brand>(_databaseSettings.BrandCollectionName);
            _productImageCollection = database.GetCollection<ProductImage>(_databaseSettings.ProductImageCollectionName);
            _specialOfferCollection = database.GetCollection<SpecialOffer>(_databaseSettings.SpecialOfferCollectionName);
            _featureCollection = database.GetCollection<Feature>(_databaseSettings.FeatureCollectionName);
        }

        public async Task<long> GetBrandCountAsync()
        {
            return await _brandCollection.CountDocumentsAsync(FilterDefinition<Brand>.Empty);
        }

        public async Task<long> GetCategoryCountAsync()
        {
            return await _categoryCollection.CountDocumentsAsync(FilterDefinition<Category>.Empty);
        }

        public async Task<string> GetMaxPriceProductNameAsync()
        {
            var filter = Builders<Product>.Filter.Empty;
            var sort = Builders<Product>.Sort.Descending(x => x.ProductPrice);
            var projection = Builders<Product>.Projection.Include(y =>
                                                y.ProductName).Exclude("ProductId");
            var product = await _productCollection.Find(filter)
                                                .Sort(sort)
                                                .Project(projection)
                                                .FirstOrDefaultAsync();
            return product.GetValue("ProductName").AsString;
        }

        public async Task<string> GetMinPriceProductNameAsync()
        {
            var filter = Builders<Product>.Filter.Empty;
            var sort = Builders<Product>.Sort.Ascending(x => x.ProductPrice);
            var projection = Builders<Product>.Projection.Include(y =>
                                                y.ProductName).Exclude("ProductId");
            var product = await _productCollection.Find(filter)
                                                .Sort(sort)
                                                .Project(projection)
                                                .FirstOrDefaultAsync();
            return product.GetValue("ProductName").AsString;
        }

        public async Task<decimal> GetProductAvgPriceAsync()
        {
            var pipeLine = new BsonDocument[]
            {
        new BsonDocument("$group", new BsonDocument
        {
            { "_id", BsonNull.Value },
            { "averagePrice", new BsonDocument("$avg", "$ProductPrice") }
        })
            };
            var result = await _productCollection.Aggregate<BsonDocument>(pipeLine).FirstOrDefaultAsync();
            if (result == null) return 0;
            return result.GetValue("averagePrice", decimal.Zero).ToDecimal();       
        }

        public async Task<long> GetProductCountAsync()
        {
            return await _productCollection.CountDocumentsAsync(FilterDefinition<Product>.Empty);
        }

        public async Task<long> GetProductImageCountAsync()
        {
            return await _productImageCollection.CountDocumentsAsync(FilterDefinition<ProductImage>.Empty);
        }

        public async Task<long> GetSpecialOfferCountAsync()
        {
            return await _specialOfferCollection.CountDocumentsAsync(FilterDefinition<SpecialOffer>.Empty);
        }

        public async Task<long> GetFeatureCountAsync()
        {
            return await _featureCollection.CountDocumentsAsync(FilterDefinition<Feature>.Empty);
        }

        public async Task<string> GetLastBrandNameAsync()
        {
            var filter = Builders<Brand>.Filter.Empty;
            var sort = Builders<Brand>.Sort.Descending(x => x.BrandId);
            var projection = Builders<Brand>.Projection.Include(y =>
                                                y.BrandName).Exclude("BrandId");
            var brand = await _brandCollection.Find(filter)
                                                .Sort(sort)
                                                .Project(projection)
                                                .FirstOrDefaultAsync();
            return brand.GetValue("BrandName").AsString;
        }

        public async Task<string> GetLastProductNameAsync()
        {
            var filter = Builders<Product>.Filter.Empty;
            var sort = Builders<Product>.Sort.Descending(x => x.ProductId);
            var projection = Builders<Product>.Projection.Include(y =>
                                                y.ProductName).Exclude("ProductId");
            var product = await _productCollection.Find(filter)
                                                .Sort(sort)
                                                .Project(projection)
                                                .FirstOrDefaultAsync();
            return product.GetValue("ProductName").AsString;
        }
    }
}
