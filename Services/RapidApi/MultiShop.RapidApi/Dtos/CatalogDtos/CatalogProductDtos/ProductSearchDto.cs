namespace MultiShop.RapidApi.Dtos.CatalogDtos.CatalogProductDtos
{
    public class ProductSearchDto
    {
        public string ProductTitle { get; set; }
        public string Price { get; set; }
        public string OriginalPrice { get; set; }
        public bool OnSale { get; set; }
        public string DiscountPercent { get; set; }
        public string ProductPageUrl { get; set; }
        public string ProductPhoto { get; set; }
        public string StoreName { get; set; }
        public bool? HasMultipleOffers { get; set; }
        public double? ProductRating { get; set; }
        public int? ProductNumReviews { get; set; }
        public string Shipping { get; set; }
    }
}
