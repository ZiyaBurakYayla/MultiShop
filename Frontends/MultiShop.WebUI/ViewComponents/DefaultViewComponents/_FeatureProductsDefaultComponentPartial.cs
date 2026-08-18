using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.CatalogServices.SellerServices;
using MultiShop.WebUI.Services.CommentServices;
using System.Linq;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class _FeatureProductsDefaultComponentPartial : ViewComponent
    {
        private readonly IProductService _productService;
        private readonly ISellerService _sellerService;
        private readonly ICommentService _commentService;

        public _FeatureProductsDefaultComponentPartial(IProductService productService, ISellerService sellerService, ICommentService commentService)
        {
            _productService = productService;
            _sellerService = sellerService;
            _commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _productService.GetAllProductsAsync();
            if (values == null)
            {
                values = new List<ResultProductDto>();
            }

            if (values.Any(x => !string.IsNullOrEmpty(x.SellerId)))
            {
                var sellers = await _sellerService.GetAllSellersAsync();
                ViewBag.SellerNames = sellers?.ToDictionary(x => x.SellerId, x => x.StoreName);
            }

            ViewBag.Ratings = await CommentRatingHelper.GetRatingSummaries(_commentService);

            return View(values);
        }
    }
}
