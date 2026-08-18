namespace MultiShop.Payment.Dtos
{
    public class CreatePaymentDto
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }
        public string Cvv { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
