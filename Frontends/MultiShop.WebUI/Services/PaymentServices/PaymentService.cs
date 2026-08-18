using System.Text.Json;
using MultiShop.DtoLayer.PaymentDtos;

namespace MultiShop.WebUI.Services.PaymentServices
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaymentResultDto> ProcessPaymentAsync(CreatePaymentDto createPaymentDto)
        {
            var response = await _httpClient.PostAsJsonAsync<CreatePaymentDto>("payments", createPaymentDto);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new PaymentResultDto
                {
                    IsSuccessful = false,
                    Message = "Ödeme servisine ulaşılamadı. Durum kodu: " + (int)response.StatusCode
                };
            }

            if (string.IsNullOrWhiteSpace(content))
            {
                return new PaymentResultDto
                {
                    IsSuccessful = false,
                    Message = "Ödeme servisinden boş yanıt alındı."
                };
            }

            var values = JsonSerializer.Deserialize<PaymentResultDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (values == null)
            {
                return new PaymentResultDto
                {
                    IsSuccessful = false,
                    Message = "Ödeme yanıtı okunamadı."
                };
            }
            return values;
        }
    }
}
