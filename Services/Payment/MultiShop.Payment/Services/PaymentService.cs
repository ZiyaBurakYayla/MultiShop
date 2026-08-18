using MultiShop.Payment.Dtos;

namespace MultiShop.Payment.Services
{
    public class PaymentService : IPaymentService
    {
        public PaymentResultDto ProcessPayment(CreatePaymentDto createPaymentDto)
        {
            var result = new PaymentResultDto();
            result.IsSuccessful = false;

            if (string.IsNullOrWhiteSpace(createPaymentDto.CardName))
            {
                result.Message = "Kart sahibi adı boş olamaz.";
                return result;
            }

            var cardNumber = "";
            if (createPaymentDto.CardNumber != null)
            {
                cardNumber = createPaymentDto.CardNumber.Replace(" ", "");
            }

            if (cardNumber.Length != 16 || !IsAllDigits(cardNumber))
            {
                result.Message = "Kart numarası 16 haneli ve sadece rakamlardan oluşmalıdır.";
                return result;
            }

            if (createPaymentDto.Cvv == null || (createPaymentDto.Cvv.Length != 3 && createPaymentDto.Cvv.Length != 4) || !IsAllDigits(createPaymentDto.Cvv))
            {
                result.Message = "CVV 3 veya 4 haneli olmalıdır.";
                return result;
            }

            int month = 0;
            int year = 0;
            bool monthOk = int.TryParse(createPaymentDto.ExpirationMonth, out month);
            bool yearOk = int.TryParse(createPaymentDto.ExpirationYear, out year);

            if (!monthOk || month < 1 || month > 12)
            {
                result.Message = "Son kullanma ayı geçersiz.";
                return result;
            }

            if (!yearOk || year < DateTime.Now.Year)
            {
                result.Message = "Son kullanma yılı geçersiz.";
                return result;
            }

            if (year == DateTime.Now.Year && month < DateTime.Now.Month)
            {
                result.Message = "Kartın son kullanma tarihi geçmiş.";
                return result;
            }

            if (createPaymentDto.TotalPrice <= 0)
            {
                result.Message = "Ödeme tutarı geçersiz.";
                return result;
            }

            result.IsSuccessful = true;
            result.Message = "Ödeme başarıyla alındı.";
            result.TransactionId = Guid.NewGuid().ToString();
            return result;
        }

        private bool IsAllDigits(string value)
        {
            foreach (var ch in value)
            {
                if (ch < '0' || ch > '9')
                {
                    return false;
                }
            }
            return true;
        }
    }
}
