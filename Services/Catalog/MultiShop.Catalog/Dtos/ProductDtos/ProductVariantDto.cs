namespace MultiShop.Catalog.Dtos.ProductDtos
{
    public class ProductVariantDto
    {
        public string Sku { get; set; }
        public Dictionary<string, string> Options { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
