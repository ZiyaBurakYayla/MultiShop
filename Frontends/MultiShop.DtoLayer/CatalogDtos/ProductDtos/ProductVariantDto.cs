using System.Collections.Generic;

namespace MultiShop.DtoLayer.CatalogDtos.ProductDtos
{
    public class ProductVariantDto
    {
        public string Sku { get; set; }
        public Dictionary<string, string> Options { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
