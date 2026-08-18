using MultiShop.DtoLayer.RapidApiDtos.CatalogProductDtos;
using MultiShop.WebUI.Models.Requests;

namespace MultiShop.WebUI.Services.RapidApiServices.CatalogProductServices
{
    public interface IProductSearchService
    {
        Task<List<ProductSearchDto>> SearchAsync(ProductSearchRequest request);
    }
}
