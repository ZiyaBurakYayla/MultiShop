using MultiShop.RapidApi.Dtos.CatalogDtos.CatalogProductDtos;
using MultiShop.RapidApi.Requests.CatalogRequests.CatalogProductRequests;

namespace MultiShop.RapidApi.Services.CatalogServices.CatalogProductServices
{
    public interface IProductService
    {
        Task<List<ProductSearchDto>> SearchAsync(ProductSearchRequest request);
    }
}
