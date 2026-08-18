using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities
{
    public class Seller
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string SellerId { get; set; }
        public string StoreName { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }
        public string OwnerUserId { get; set; }
        public string Status { get; set; }
    }
}
