using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.DtoLayer.OrderDtos.OrderDetailDtos;
using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.CatalogServices.SellerServices;
using MultiShop.WebUI.Services.Concrete;
using MultiShop.WebUI.Services.OrderServices.OrderDetailServices;

namespace MultiShop.WebUI.Areas.Seller.Controllers
{
    [Area("Seller")]
    [Authorize(Roles = "Seller")]
    [Route("Seller/Dashboard")]
    public class DashboardController : Controller
    {
        private readonly ISellerService _sellerService;
        private readonly IProductService _productService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly ILoginService _loginService;

        public DashboardController(ISellerService sellerService, IProductService productService, IOrderDetailService orderDetailService, ILoginService loginService)
        {
            _sellerService = sellerService;
            _productService = productService;
            _orderDetailService = orderDetailService;
            _loginService = loginService;
        }

        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var sellers = await _sellerService.GetAllSellersAsync();
            var seller = sellers?.FirstOrDefault(x => x.OwnerUserId == _loginService.GetUserId && x.Status == "Approved");
            if (seller == null)
            {
                return RedirectToAction("Index", "SellerApplication", new { area = "User" });
            }

            var products = await _productService.GetProductsBySellerIdAsync(seller.SellerId);
            if (products == null)
            {
                products = new List<ResultProductDto>();
            }

            var orderDetails = await _orderDetailService.GetOrderDetailsBySellerIdAsync(seller.SellerId);
            if (orderDetails == null)
            {
                orderDetails = new List<ResultOrderDetailBySellerDto>();
            }

            var model = new SellerDashboardViewModel
            {
                Seller = seller,
                Products = products,
                RecentOrders = orderDetails.Take(5).ToList(),
                TopProducts = BuildTopProducts(orderDetails)
            };

            foreach (var item in orderDetails)
            {
                model.SoldQuantity += item.ProductAmount;
                if (item.IsPaid)
                {
                    model.PaidRevenue += item.ProductTotalPrice;
                }
                else
                {
                    model.PendingRevenue += item.ProductTotalPrice;
                }
            }

            model.OrderCount = orderDetails.Select(x => x.OrderingId).Distinct().Count();
            if (model.OrderCount > 0)
            {
                model.AverageOrderValue = (model.PaidRevenue + model.PendingRevenue) / model.OrderCount;
            }

            return View(model);
        }

        private List<SellerTopProductViewModel> BuildTopProducts(List<ResultOrderDetailBySellerDto> orderDetails)
        {
            var map = new Dictionary<string, SellerTopProductViewModel>();
            foreach (var item in orderDetails)
            {
                var key = item.ProductName ?? "";
                if (!map.ContainsKey(key))
                {
                    map[key] = new SellerTopProductViewModel { ProductName = key };
                }
                map[key].Quantity += item.ProductAmount;
                map[key].Revenue += item.ProductTotalPrice;
            }
            return map.Values.OrderByDescending(x => x.Quantity).Take(5).ToList();
        }
    }
}
