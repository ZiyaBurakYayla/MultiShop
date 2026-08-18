using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.CatalogServices.SellerServices;
using MultiShop.WebUI.Services.CommentServices;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponents
{
    public class _ProductDetailFeatureComponentPartial : ViewComponent
    {
        private readonly IProductService _productService;
        private readonly ISellerService _sellerService;
        private readonly ICommentService _commentService;

        public _ProductDetailFeatureComponentPartial(IProductService productService, ISellerService sellerService, ICommentService commentService)
        {
            _productService = productService;
            _sellerService = sellerService;
            _commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var value = await _productService.GetProductByIdAsync(id);
            if(value == null)
            {
                return View("Error");
            }

            if (!string.IsNullOrEmpty(value.SellerId))
            {
                var seller = await _sellerService.GetSellerByIdAsync(value.SellerId);
                if (seller != null)
                {
                    ViewBag.SellerId = seller.SellerId;
                    ViewBag.SellerName = seller.StoreName;
                }
            }

            var comments = await _commentService.CommentsListByProductId(id);
            ViewBag.RatingCount = comments.Count;
            ViewBag.RatingAverage = comments.Count == 0 ? 0 : comments.Average(x => x.Rating);

            return View(value);
        }
    }
}
