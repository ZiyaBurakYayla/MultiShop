using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Payment.Dtos;
using MultiShop.Payment.Services;

namespace MultiShop.Payment.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public IActionResult ProcessPayment(CreatePaymentDto createPaymentDto)
        {
            var result = _paymentService.ProcessPayment(createPaymentDto);
            return Ok(result);
        }
    }
}
