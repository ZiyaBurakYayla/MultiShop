using MultiShop.DtoLayer.CatalogDtos.ProductDetailDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;

namespace MultiShop.WebUI.Models
{
    public class AdminProductViewModel
    {
        public UpdateProductDto Product { get; set; }
        public GetByIdProductDetailDto Detail { get; set; }
        public UpdateProductImageDto Images { get; set; }
        public string CategoryName { get; set; }
        public string SellerName { get; set; }
    }
}
