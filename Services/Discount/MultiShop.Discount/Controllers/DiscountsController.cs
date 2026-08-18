using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Discount.Dtos;
using MultiShop.Discount.Services;

namespace MultiShop.Discount.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountsController(IDiscountService discountService)
        {
            _discountService = discountService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCoupons()
        {
            var coupons = await _discountService.GetAllCouponAsync();
            return Ok(coupons);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCouponById(int id)
        {
            var coupon = await _discountService.GetByIdCouponAsync(id);
            return Ok(coupon);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCoupon(CreateCouponDto coupon)
        {
            await _discountService.CreateCouponAsync(coupon);
            return Ok("Başarılı");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCoupon(UpdateCouponDto coupon)
        {
            await _discountService.UpdateCouponAsync(coupon);
            return Ok("Başarılı");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCoupon(int id)
        {
            await _discountService.DeleteCouponAsync(id);
            return Ok("Başarılı");
        }

        [HttpGet("GetCodeDetailByCodeAsync")]
        public async Task<IActionResult> GetCodeDetailByCodeAsync(string code)
        {
            var value = await _discountService.GetCodeDetailByCodeAsync(code);
            return Ok(value);
        }

        [HttpGet("GetDiscountCouponRate")]
        public IActionResult GetDiscountCouponRate(string code)
        {
            var value =  _discountService.GetDiscountCouponRate(code);
            return Ok(value);
        }
    }
}
