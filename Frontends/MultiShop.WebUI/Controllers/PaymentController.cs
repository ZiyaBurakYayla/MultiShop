using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CargoDtos.CargoDetailDtos;
using MultiShop.DtoLayer.OrderDtos.OrderDetailDtos;
using MultiShop.DtoLayer.OrderDtos.OrderingDtos;
using MultiShop.DtoLayer.PaymentDtos;
using MultiShop.WebUI.RabbitMQ;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.CargoServices.CargoCompanyServices;
using MultiShop.WebUI.Services.CargoServices.CargoDetailServices;
using MultiShop.WebUI.Services.IdentityServices.UserServices;
using MultiShop.WebUI.Services.OrderServices.OrderDetailServices;
using MultiShop.WebUI.Services.OrderServices.OrderingServices;
using MultiShop.WebUI.Services.PaymentServices;

namespace MultiShop.WebUI.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IOrderingService _orderingService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IUserService _userService;
        private readonly IPaymentService _paymentService;
        private readonly ICargoDetailService _cargoDetailService;
        private readonly ICargoCompanyService _cargoCompanyService;
        private readonly RabbitMQPublisher _rabbitMQPublisher;
        public PaymentController(IBasketService basketService, IOrderingService orderingService, 
            IOrderDetailService orderDetailService, IUserService userService, IPaymentService paymentService,
            ICargoDetailService cargoDetailService, ICargoCompanyService cargoCompanyService,
            RabbitMQPublisher rabbitMQPublisher)
        {
            _basketService = basketService;
            _orderingService = orderingService;
            _orderDetailService = orderDetailService;
            _userService = userService;
            _paymentService = paymentService;
            _cargoDetailService = cargoDetailService;
            _cargoCompanyService = cargoCompanyService;
            _rabbitMQPublisher = rabbitMQPublisher;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "Ödeme Ekranı";
            ViewBag.directory3 = "Kart İle Ödeme";
            ViewBag.CargoCompanies = await _cargoCompanyService.GetAllCargoCompanyAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CompleteOrder(CreatePaymentDto createPaymentDto, int cargoCompanyId)
        {
            var user = await _userService.GetUserInfo();
            var basket = await _basketService.GetBasket();

            if (basket.BasketItems == null || basket.BasketItems.Count == 0)
            {
                return RedirectToAction("Index", "ShoppingCart");
            }

            if (cargoCompanyId == 0)
            {
                TempData["PaymentError"] = "Kargo firması seçmelisiniz.";
                return RedirectToAction("Index");
            }

            createPaymentDto.TotalPrice = basket.TotalPrice;
            var paymentResult = await _paymentService.ProcessPaymentAsync(createPaymentDto);

            if (paymentResult == null || !paymentResult.IsSuccessful)
            {
                TempData["PaymentError"] = paymentResult == null ? "Ödeme sırasında bir hata oluştu." : paymentResult.Message;
                return RedirectToAction("Index");
            }

            await _orderingService.CreateOrdering(new CreateOrderingDto
            {
                UserId = user.Id,
                TotalPrice = basket.TotalPrice,
                OrderDate = DateTime.Now,
                IsPaid = true
            });

            var orderings = await _orderingService.GetOrderingByUserId(user.Id);
            int orderingId = 0;
            foreach (var ordering in orderings)
            {
                if (ordering.OrderingId > orderingId)
                {
                    orderingId = ordering.OrderingId;
                }
            }

            var detailErrors = new List<string>();
            foreach (var item in basket.BasketItems)
            {
                var detailError = await _orderDetailService.CreateOrderDetail(new CreateOrderDetailDto
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    ProductPrice = item.Price,
                    ProductAmount = item.Quantity,
                    ProductTotalPrice = item.Price * item.Quantity,
                    OrderingId = orderingId,
                    Sku = string.IsNullOrEmpty(item.Sku) ? "-" : item.Sku,
                    SellerId = string.IsNullOrEmpty(item.SellerId) ? "-" : item.SellerId
                });
                if (detailError != null)
                {
                    detailErrors.Add(item.ProductName + ": " + detailError);
                }
            }

            if (detailErrors.Count > 0)
            {
                TempData["PaymentError"] = "Sipariş kalemleri kaydedilemedi. " + string.Join(" | ", detailErrors);
                return RedirectToAction("Index");
            }

            await _cargoDetailService.CreateCargoDetailAsync(new CreateCargoDetailDto
            {
                SenderCustomer = "MultiShop",
                ReceiverCustomer = user.Name + " " + user.Surname,
                Barcode = "MS" + DateTime.Now.ToString("yyyyMMdd") + orderingId,
                CargoCompanyId = cargoCompanyId,
                OrderingId = orderingId,
                UserId = user.Id,
                Status = "Hazırlanıyor"
            });

            await _basketService.DeleteBasket(user.Id);

            if (!string.IsNullOrWhiteSpace(user.Email))
            {
                try
                {
                    var items = new List<OrderCreatedItem>();
                    foreach (var item in basket.BasketItems)
                    {
                        items.Add(new OrderCreatedItem
                        {
                            ProductName = item.ProductName,
                            Quantity = item.Quantity,
                            Price = item.Price
                        });
                    }

                    _rabbitMQPublisher.Publish(new OrderCreatedEvent
                    {
                        CustomerEmail = user.Email,
                        CustomerName = user.Name,
                        TotalPrice = basket.TotalPrice,
                        TransactionId = paymentResult.TransactionId,
                        OrderDate = DateTime.Now,
                        Items = items
                    });
                }
                catch
                {
                }
            }


            TempData["PaymentSuccess"] = "Ödemeniz alındı. İşlem numarası: " + paymentResult.TransactionId;
            return RedirectToAction("MyOrderList", "MyOrder", new { area = "User" });
        }
    }
}
