using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.CatalogServices.SellerServices;
using MultiShop.WebUI.Services.CommentServices;
using System.Linq;

namespace MultiShop.WebUI.ViewComponents.ProductListViewComponents
{
    public class _ProductListComponentPartial : ViewComponent
    {
        private readonly IProductService _productService;
        private readonly ISellerService _sellerService;
        private readonly ICommentService _commentService;

        public _ProductListComponentPartial(IProductService productService, ISellerService sellerService, ICommentService commentService)
        {
            _productService = productService;
            _sellerService = sellerService;
            _commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            List<ResultProductWithCategoryDto> values;
            if (string.IsNullOrEmpty(id))
            {
                values = await _productService.GetProductWithCategoryAsync();
            }
            else
            {
                values = await _productService.GetProductWithCategoryByCategoryIdAsync(id);
            }
            if (values == null)
            {
                values = new List<ResultProductWithCategoryDto>();
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
