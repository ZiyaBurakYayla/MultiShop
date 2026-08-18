using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.DtoLayer.CatalogDtos.SellerDtos;

namespace MultiShop.WebUI.Models
{
    public class StoreViewModel
    {
        public UpdateSellerDto Seller { get; set; }
        public List<ResultProductDto> Products { get; set; }
    }
}
