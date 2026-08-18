namespace MultiShop.DtoLayer.PaymentDtos
{
    public class PaymentResultDto
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public string TransactionId { get; set; }
    }
}
