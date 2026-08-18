using Microsoft.AspNetCore.SignalR;
using MultiShop.SignalRRealTimeApi.Services.Catalogs.SignalRBrandServices;
using MultiShop.SignalRRealTimeApi.Services.Catalogs.SignalRProductServices;
using MultiShop.SignalRRealTimeApi.Services.Catalogs.SignalRCategoryServices;
using MultiShop.SignalRRealTimeApi.Services.Catalogs.SignalRSpecialOfferServices;
using MultiShop.SignalRRealTimeApi.Services.Catalogs.SignalRProductImageServices;
using MultiShop.SignalRRealTimeApi.Services.Catalogs.SignalRFeatureServices;
using MultiShop.SignalRRealTimeApi.Services.Comments.SignalRCommentServices;
using MultiShop.SignalRRealTimeApi.Services.Messages.SignalRMessageServices;
using MultiShop.SignalRRealTimeApi.Services.Orders.SignalROrderServices;
using MultiShop.SignalRRealTimeApi.Services.Cargos.SignalRCargoServices;
using MultiShop.SignalRRealTimeApi.Services.Discounts.SignalRDiscountServices;
using MultiShop.SignalRRealTimeApi.Services.Users.SignalRUserServices;

namespace MultiShop.SignalRRealTimeApi.Hubs
{
    public class SignalRHub : Hub
    {
        private readonly ISignalRMessageService _signalRMessageService;
        private readonly ISignalRCommentService __signalRCommentService;
        private readonly ISignalRBrandService _signalRBrandService;
        private readonly ISignalRProductService _signalRProductService;
        private readonly ISignalRCategoryService _signalRCategoryService;
        private readonly ISignalRSpecialOfferService _signalRSpecialOfferService;
        private readonly ISignalRProductImageService _signalRProductImageService;
        private readonly ISignalRFeatureService _signalRFeatureService;
        private readonly ISignalROrderService _signalROrderService;
        private readonly ISignalRCargoService _signalRCargoService;
        private readonly ISignalRDiscountService _signalRDiscountService;
        private readonly ISignalRUserService _signalRUserService;

        public SignalRHub(
            ISignalRMessageService signalRMessageService,
            ISignalRCommentService signalRCommentService,
            ISignalRBrandService signalRBrandService,
            ISignalRProductService signalRProductService,
            ISignalRCategoryService signalRCategoryService,
            ISignalRSpecialOfferService signalRSpecialOfferService,
            ISignalRProductImageService signalRProductImageService,
            ISignalRFeatureService signalRFeatureService,
            ISignalROrderService signalROrderService,
            ISignalRCargoService signalRCargoService,
            ISignalRDiscountService signalRDiscountService,
            ISignalRUserService signalRUserService)
        {
            _signalRMessageService = signalRMessageService;
            __signalRCommentService = signalRCommentService;
            _signalRBrandService = signalRBrandService;
            _signalRProductService = signalRProductService;
            _signalRCategoryService = signalRCategoryService;
            _signalRSpecialOfferService = signalRSpecialOfferService;
            _signalRProductImageService = signalRProductImageService;
            _signalRFeatureService = signalRFeatureService;
            _signalROrderService = signalROrderService;
            _signalRCargoService = signalRCargoService;
            _signalRDiscountService = signalRDiscountService;
            _signalRUserService = signalRUserService;
        }

        public async Task SendStatisticCount()
        {
            var getTotalComment = await __signalRCommentService.GetCommentCountAsync();
            await Clients.All.SendAsync("ReceiveCommentCount", getTotalComment);

            var getActiveComment = await __signalRCommentService.GetActiveCommentCountAsync();
            await Clients.All.SendAsync("ReceiveActiveCommentCount", getActiveComment);

            var getPassiveComment = await __signalRCommentService.GetPassiveCommentCountAsync();
            await Clients.All.SendAsync("ReceivePassiveCommentCount", getPassiveComment);

            var getTotalMessageCount = await _signalRMessageService.GetTotalMessageCountAsync();
            await Clients.All.SendAsync("ReceiveTotalMessageCount", getTotalMessageCount);
        }

        public async Task SendCatalogStatics()
        {
            //Brand
            var BrandCount = await _signalRBrandService.GetBrandCountAsync();
            await Clients.All.SendAsync("ReceiveBrandCount", BrandCount);
            var LastBrandName = await _signalRBrandService.GetLastBrandNameAsync();
            await Clients.All.SendAsync("ReceiveLastBrandName", LastBrandName);

            //Product
            var ProductCount = await _signalRProductService.GetProductCountAsync();
            await Clients.All.SendAsync("ReceiveProductCount", ProductCount);
            var LastProductName = await _signalRProductService.GetLastProductNameAsync();
            await Clients.All.SendAsync("ReceiveLastProductName", LastProductName);
            var MinPriceProductName = await _signalRProductService.GetMinPriceProductNameAsync();
            await Clients.All.SendAsync("ReceiveMinPriceProductName", MinPriceProductName);
            var ProductAvgPrice = await _signalRProductService.GetProductAvgPriceAsync();
            await Clients.All.SendAsync("ReceiveProductAvgPrice", ProductAvgPrice);
            var MaxPriceProductName = await _signalRProductService.GetMaxPriceProductNameAsync();
            await Clients.All.SendAsync("ReceiveMaxPriceProductName", MaxPriceProductName);

            //Category
            var CategoryCount = await _signalRCategoryService.GetCategoryCountAsync();
            await Clients.All.SendAsync("ReceiveCategoryCount", CategoryCount);

            //SpecialOffer
            var SpecialOfferCount = await _signalRSpecialOfferService.GetSpecialOfferCountAsync();
            await Clients.All.SendAsync("ReceiveSpecialOfferCount", SpecialOfferCount);

            //ProductImage
            var ProductImageCount = await _signalRProductImageService.GetProductImageCountAsync();
            await Clients.All.SendAsync("ReceiveProductImageCount", ProductImageCount);

            //Feature
            var FeatureCount = await _signalRFeatureService.GetFeatureCountAsync();
            await Clients.All.SendAsync("ReceiveFeatureCount", FeatureCount);
        }

        public async Task SendOrderStatics()
        {
            var TotalOrderCount = await _signalROrderService.GetTotalOrderCountAsync();
            await Clients.All.SendAsync("ReceiveTotalOrderCount", TotalOrderCount);

            var TotalOrderDetailCount = await _signalROrderService.GetTotalOrderDetailCountAsync();
            await Clients.All.SendAsync("ReceiveTotalOrderDetailCount", TotalOrderDetailCount);
        }

        public async Task SendCargoStatics()
        {
            var TotalCargoCustomerCount = await _signalRCargoService.GetTotalCargoCustomerCountAsync();
            await Clients.All.SendAsync("ReceiveTotalCargoCustomerCount", TotalCargoCustomerCount);
        }

        public async Task SendDiscountStatics()
        {
            var DiscountCouponCount = await _signalRDiscountService.GetDiscountCouponCountAsync();
            await Clients.All.SendAsync("ReceiveDiscountCouponCount", DiscountCouponCount);
        }

        public async Task SendUserStatics()
        {
            var UserCount = await _signalRUserService.GetUserCountAsync();
            await Clients.All.SendAsync("ReceiveUserCount", UserCount);
        }
    }
}
