namespace MultiShop.Catalog.Entities
{
    public class ProductVariant
    {
        public string Sku { get; set; }
        public Dictionary<string, string> Options { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
