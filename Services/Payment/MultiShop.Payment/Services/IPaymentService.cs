using MultiShop.Payment.Dtos;

namespace MultiShop.Payment.Services
{
    public interface IPaymentService
    {
        PaymentResultDto ProcessPayment(CreatePaymentDto createPaymentDto);
    }
}
